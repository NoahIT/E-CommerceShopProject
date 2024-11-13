using BL.Auth;
using BL.Models;
using DAL.Interfaces;
using DAL.Models;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BL.General;

namespace BL.Cart
{
    public class Cart : ICart
    {
        private readonly ICartDAL cartDAL;
        private readonly ICurrentUser currentUser;
        private readonly IDbSession dbsession;
        private readonly IOrderDAL orderDAL;

        public Cart(ICartDAL cartDAL, ICurrentUser currentUser, IDbSession dbsession, IOrderDAL orderDal)
        {
            this.currentUser = currentUser;
            this.cartDAL = cartDAL;
            this.dbsession = dbsession;
            orderDAL = orderDal;
        }

        public async Task<CartModel> GetCurrentUserCartModel()
        {
            bool isLoggedIn = await currentUser.IsLoggedIn();
            if (isLoggedIn)
            {
                int? userId = await currentUser.GetCurrentUserId();
                return await this.cartDAL.GetCart((int)userId);
            }
            else
            {
                var session = await dbsession.GetSession();
                return await this.cartDAL.GetCart(session.DbSessionId);
            }
        }

        public async Task<CartModel> CreateOrGetCurrentUserCartModel()
        {
            var cartModel = await GetCurrentUserCartModel();
            if (cartModel.CartId == null)
                cartModel.CartId = await CreateCurrentUserCart();
            return cartModel;
        }

        public async Task<UserCartModel> GetCurrentUserCart()
        {
            var cartModel = await GetCurrentUserCartModel();

            if (cartModel.CartId == null)
                return new UserCartModel()
                {
                    Cart = cartModel
                };

            var cartItems = (await this.cartDAL.GetCartItems((int)cartModel.CartId)).ToList();

            return new UserCartModel()
            {
                Cart = cartModel,
                Items = cartItems,
                Discount = await cartDAL.GetDiscountById(cartModel.DiscountId)
            };
        }

        public async Task AddCurrentUserCartProduct(int productId)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, Constants.TRANSACTION_SECCONDS),
                TransactionScopeAsyncFlowOption.Enabled
                ))
            {
                await this.dbsession.Lock();

                CartModel cartModel = await CreateOrGetCurrentUserCartModel();

                CartItemModel? cartItemModel = (await this.cartDAL.GetCartItems(cartModel.CartId ?? 0)).FirstOrDefault(m => m.ProductId == productId);
                if (cartItemModel == null)
                {
                    cartItemModel = new CartItemModel()
                    {
                        CartId = cartModel.CartId ?? 0,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        ProductId = productId,
                        ProductCount = 1
                    };
                    await cartDAL.AddCartItem(cartItemModel);
                }
                else
                {
                    cartItemModel.Modified = DateTime.Now;
                    cartItemModel.ProductCount = cartItemModel.ProductCount + 1;
                    await cartDAL.UpdateCartItem(cartItemModel);
                }
                scope.Complete();
            }

            await UpdateSessionCartValue();
        }

        public async Task<int> CreateCurrentUserCart()
        {
            CartModel cartModel = new CartModel();
            cartModel.Modified = DateTime.Now;
            cartModel.Created = DateTime.Now;

            if (await currentUser.IsLoggedIn())
                cartModel.UserId = await currentUser.GetCurrentUserId();
            cartModel.SessionId = (await dbsession.GetSession()).DbSessionId;

            return await cartDAL.CreateCart(cartModel);
        }

        public async Task UpdateCurrentUserCartProduct(int productId, int productCount)
        {
            CartModel cartModel = await CreateOrGetCurrentUserCartModel();

            CartItemModel? cartItemModel = (await this.cartDAL.GetCartItems(cartModel.CartId ?? 0)).FirstOrDefault(m => m.ProductId == productId);
            if (cartItemModel == null)
                return;
            if (productCount > 0)
            {
                cartItemModel.Modified = DateTime.Now;
                cartItemModel.ProductCount = productCount;
                await cartDAL.UpdateCartItem(cartItemModel);
            }
            else
                await cartDAL.DeleteCartItem(cartItemModel.CartItemId);

            await UpdateSessionCartValue();
        }

        public async Task MergeCart(Guid oldSessionId, int userId)
        {
            var cartModel = await GetCurrentUserCartModel();
            if (cartModel.CartId == null)
                await cartDAL.SetCartUserId(oldSessionId, userId);
            else
                await cartDAL.MoveCartItems(oldSessionId, (int)cartModel.CartId);

            await UpdateSessionCartValue();
        }

        public async Task UpdateSessionCartValue()
        {
            var model = await GetCurrentUserCart();
            dbsession.AddValue(Constants.CART_PARAM_NAME, model.Count);
            await dbsession.UpdateSessionData();
        }

        public async Task UpdateCurrentUserCart(CartModel model)
        {
            await this.cartDAL.UpdateCart(model);
        }

        public async Task<bool> PlaceOrder()
        {
            using (var scope = Helpers.CreateTransactionScope())
            {
                if (!ChargeCreditCard())
                    return false;

                var cartModel = await GetCurrentUserCart();
                await CreateOrder(cartModel);
                await cartDAL.ArchiveCart(cartModel.Cart.CartId ?? 0);

                scope.Complete();
            }

            return true;
        }

        public bool ChargeCreditCard()
        {
            return true;
        }

        public async Task CreateOrder(UserCartModel model)
        {
            // скопировать всё из CartItem в OrderItem
            // скопировать из Cart в Order то, что необходимо BillindId, AddressId
            var orderModel = new OrderModel()
            {
                AddressId = model.Cart.AddressId,
                OrderId = model.Cart.CartId,
                UserId = model.Cart.UserId,
                SessionId = model.Cart.SessionId,
                Discount = model.Discount
            };

            var orderId = await orderDAL.CreateOrder(orderModel);

            foreach (var item in model.Items)
            {
                var itemModel = new OrderItemModel()
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    ProductCount = item.ProductCount,
                    Price = item.Price * item.ProductCount
                };

                await orderDAL.CreateOrderItem(itemModel);

            }
        }

        public async Task<bool> UpdateCartDiscountId(int cartId, string discountCode)
        {
            var discount = await cartDAL.GetDiscountIdByCode(discountCode);

            if (discount==null)
            {
                return false;
            }

            await cartDAL.UpdateDiscount(cartId, (int)discount);

            return true;
        }

        public async Task DeleteDiscountId(int cartId)
        {
            await cartDAL.DeleteDiscount(cartId);
        }
    }
}

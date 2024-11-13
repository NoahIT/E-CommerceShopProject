using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using ZstdSharp.Unsafe;

namespace DAL.Cart
{
    public class CartDAL : ICartDAL
    {
        private readonly IDbHelper dbHelper;

        public CartDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<int> CreateCart(CartModel model)
        {
            return await dbHelper.QueryScalarAsync<int>(@"
                    insert into Cart (SessionId, UserId, Created, Modified, isArchived, AddressId)
                    values (@SessionId, @UserId, @Created, @Modified,0,@AddressId);
                    SELECT LAST_INSERT_ID();", model);
        }

        public async Task<CartModel> GetCart(Guid sessionId)
        {
            return (await dbHelper.QueryScalarAsync<CartModel>(@"
                    select CartId, SessionId, UserId, Created, Modified, isArchived, AddressId, DiscountId
                    from Cart
                    where SessionId = @sessionId AND isArchived = 0", new { sessionId = sessionId })) ?? new CartModel() { SessionId = sessionId };
        }

        public async Task<CartModel> GetCart(int userId)
        {
            return (await dbHelper.QueryScalarAsync<CartModel>(@"
                    select CartId, SessionId, UserId, Created, Modified, AddressId, DiscountId
                    from Cart
                    where UserId = @userId AND isArchived = 0", new { userId = userId })) ?? new CartModel() { UserId = userId };
        }

        public async Task<int> AddCartItem(CartItemModel model)
        {
            return await dbHelper.QueryScalarAsync<int>(@"
                    insert into CartItem (CartId, ProductId, Created, Modified, ProductCount)
                    values (@CartId, @ProductId, @Created, @Modified, @ProductCount);
                    SELECT LAST_INSERT_ID();", model);
        }

        public async Task<IEnumerable<CartItemDetailsModel>> GetCartItems(int cartId)
        {
            return await dbHelper.QueryAsync<CartItemDetailsModel>(@"
                    select ci.CartItemId, ci.CartId, ci.ProductId, ci.Created, ci.Modified, ci.ProductCount,
                        p.Price, s.Photo, p.Model, s.SeriesName
                    from CartItem ci
                      join Product p on ci.ProductId = p.ProductId
                      join Series s on p.SeriesID = s.idSeries
                      join Cart j on ci.CartId = j.CartId
                    where ci.CartId = @cartId AND j.isArchived = 0", new { cartId = cartId });
        }

        public async Task UpdateCartItem(CartItemModel model)
        {
            await dbHelper.ExecuteAsync(@"
                    update CartItem ci
                    set Modified = UTC_TIMESTAMP(), ProductCount = @ProductCount
                    where CartItemId = @CartItemId", model);
        }

        public async Task DeleteCartItem(int cartItemId)
        {
            await dbHelper.ExecuteAsync(@"
                    delete from CartItem 
                    where CartItemId = @cartItemId", new { cartItemId = cartItemId });
        }

        public async Task SetCartUserId(Guid oldSessionId, int userId)
        {
            await dbHelper.ExecuteAsync(@"
                    update Cart 
                    set UserId = @userId
                    where SessionId = @oldSessionId
                ", new { oldSessionId = oldSessionId, userId = userId });
        }

        public async Task UpdateCart(CartModel model)
        {
            await dbHelper.ExecuteAsync(@"
                    update Cart 
                    set AddressId = @AddressId
                    where CartId = @CartId AND isArchived = 0
                ", model);
        }

        public async Task MoveCartItems(Guid oldSessionId, int newcartId)
        {
            await dbHelper.ExecuteAsync(@"
                    update CartItem 
                    set CartId = @newcartId 
                    where CartId = (select CartId from Cart where SessionId = @oldSessionId)
                ", new { oldSessionId = oldSessionId, newcartId = newcartId });
        }

        public async Task ArchiveCart(int cartId)
        {
            await dbHelper.ExecuteAsync(@"
                    update Cart 
                    set isArchived = @value
                    where CartId = @cartId 
                ", new { cartId = cartId , value = 1});
        }

        public async Task<int> GetDiscountByCode(string code)
        {
            string sql = @"select idDiscount,Code,Precent from DiscountCode where Code = @Code ";

            var disc = await dbHelper.QueryScalarAsync<DiscountModel?>(sql, new { Code = code });

            if (disc == null)
            {
                return 0;
            }

            return disc.Precent;
        }
        public async Task<int> GetDiscountById(int code = 0)
        {
            string sql = @"select idDiscount,Code,Precent from DiscountCode where idDiscount = @Code ";

            var disc = await dbHelper.QueryScalarAsync<DiscountModel?>(sql, new { Code = code });

            if (disc == null)
            {
                return 0;
            }

            return disc.Precent;
        }

        public async Task UpdateDiscount(int cartId,int discountId)
        {
            await dbHelper.ExecuteAsync(@"
                    update Cart 
                    set DiscountId = @discountId
                    where CartId = @cartId 
                ", new { cartId = cartId, discountId = discountId });
        }

        public async Task DeleteDiscount(int cartId)
        {
            await dbHelper.ExecuteAsync(@"
                    update Cart 
                    set DiscountId = NULL
                    where CartId = @cartId 
                ", new { cartId = cartId });
        }

        public async Task<int?> GetDiscountIdByCode(string code)
        {
            string sql = @"select idDiscount from DiscountCode where Code = @Code ";

            var disc = await dbHelper.QueryScalarAsync<int?>(sql, new { Code = code });

            return disc;
        }
    }
}

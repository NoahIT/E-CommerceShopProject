using BL.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Cart
{
    public interface ICart
    {
        Task<UserCartModel> GetCurrentUserCart();

        Task AddCurrentUserCartProduct(int productId);

        Task UpdateCurrentUserCartProduct(int productId, int productCount);

        Task MergeCart(Guid oldSessionId, int userId);

        Task UpdateCurrentUserCart(CartModel model);
        Task<bool> PlaceOrder();
        Task<bool> UpdateCartDiscountId(int cartId, string discountId);
        Task DeleteDiscountId(int cartId);
    }
}

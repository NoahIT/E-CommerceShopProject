using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface ICartDAL
    {
        Task<CartModel> GetCart(Guid sessionId);

        Task<CartModel> GetCart(int userId);

        Task<int> CreateCart(CartModel model);

        Task<IEnumerable<CartItemDetailsModel>> GetCartItems(int cartId);

        Task<int> AddCartItem(CartItemModel model);

        Task UpdateCartItem(CartItemModel model);

        Task DeleteCartItem(int cartItemId);

        Task SetCartUserId(Guid oldSessionId, int userId);

        Task MoveCartItems(Guid oldSessionId, int newcartId);

        Task UpdateCart(CartModel model);
        Task ArchiveCart(int cartId);
        Task<int> GetDiscountByCode(string code);
        Task<int?> GetDiscountIdByCode(string code);
        Task<int> GetDiscountById(int code = 0);

        Task UpdateDiscount(int cartId, int discountId);
        Task DeleteDiscount(int cartId);
    }
}

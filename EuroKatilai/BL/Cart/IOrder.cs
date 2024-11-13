using BL.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Cart
{
    public interface IOrder
    {
        Task<List<UserOrderModel>> GetAllOrders(int userId);
        Task<UserOrderModel> GetOrder(int orderId);
        Task<List<OrderModel>?> GetOrderModels(int userId);
    }
}

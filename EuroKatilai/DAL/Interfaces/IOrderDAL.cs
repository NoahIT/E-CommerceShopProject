using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Mysqlx.Crud;

namespace DAL.Interfaces
{
    public interface IOrderDAL
    {
        Task<int> CreateOrder(OrderModel model);
        Task CreateOrderItem(OrderItemModel model);

        Task<List<OrderModel>?> GetOrders(int userId);
        Task<IEnumerable<OrderItemDetailsModel>>  GetOrderItems(int orderId);
        Task<OrderModel> GetOrderModel(int orderId);
    }
}

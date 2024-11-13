using BL.Models;
using DAL.Cart;
using DAL.Interfaces;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Cart
{
    public class Order : IOrder
    {
        public readonly IOrderDAL orderDAL;

        public Order(IOrderDAL orderDal)
        {
            orderDAL = orderDal;
        }


        public async Task<List<UserOrderModel>> GetAllOrders(int userId)
        {
            List<UserOrderModel> orders = new List<UserOrderModel>();

            var ordersFromDb = await orderDAL.GetOrders(userId);

            foreach (var order in ordersFromDb)
            {
                var orderItems = (await this.orderDAL.GetOrderItems((int)order.OrderId)).ToList();

                orders.Add(new UserOrderModel()
                {
                    Cart = order,
                    Items = orderItems,
                    Discount = order.Discount
                });

            }

            return orders;
        }

        public async Task<UserOrderModel> GetOrder(int orderId)
        {
            var cartModel = await orderDAL.GetOrderModel(orderId);

            var cartItems = (await this.orderDAL.GetOrderItems((int)cartModel.OrderId)).ToList();

            return new UserOrderModel()
            {
                Cart = cartModel,
                Items = cartItems,
                Discount = cartModel.Discount
            };
        }

        public async Task<List<OrderModel>?> GetOrderModels(int userId)
        {
            return await orderDAL.GetOrders(userId);
        }
    }
}

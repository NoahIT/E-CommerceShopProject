using DAL.Interfaces;
using DAL.Models;

namespace DAL.Cart
{
    public class OrderDAL : IOrderDAL
    {
        private readonly IDbHelper dbHelper;

        public OrderDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<int> CreateOrder(OrderModel model)
        {
            string sql = @"insert into `Order`(SessionId, UserId, AddressId, Created, Modified, Status, Discount)
                            values(@SessionId,@UserId,@AddressId,UTC_TIMESTAMP(),UTC_TIMESTAMP(),'Užsakymas apdorojimas..',@Discount);
                            SELECT LAST_INSERT_ID();";

            return await dbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task CreateOrderItem(OrderItemModel model)
        {
            string sql = @"insert into `OrderItem`(OrderId, ProductId, ProductCount, Price, Modified, Created)
                            values(@OrderId,@ProductId,@ProductCount,Price,UTC_TIMESTAMP(),UTC_TIMESTAMP())";

            await dbHelper.ExecuteAsync(sql, model);
        }

        public async Task<List<OrderModel>?> GetOrders(int userId)
        {
            string sql = @"select OrderId,SessionId,UserId,AddressId,Created,Modified,Status,Discount
                            from `Order`
                            where UserId = @userId";

            return (await dbHelper.QueryAsync<OrderModel>(sql,new{userId = userId})).ToList();
        }

        public async Task<IEnumerable<OrderItemDetailsModel>> GetOrderItems(int orderId)
        {
            return await dbHelper.QueryAsync<OrderItemDetailsModel>(@"
                    select ci.OrderItemId, ci.OrderId, ci.ProductId, ci.Created, ci.Modified, ci.ProductCount,
                        p.Price, s.Photo, p.Model, s.SeriesName, s.idSeries
                    from OrderItem ci
                      join Product p on ci.ProductId = p.ProductId
                      join Series s on p.SeriesID = s.idSeries
                      join `Order` j on ci.OrderId = j.OrderId
                    where ci.OrderId = @orderId", new { orderId = orderId });
        }

        public async Task<OrderModel> GetOrderModel(int orderId)
        {
            string sql = @"select OrderId,SessionId,UserId,AddressId,Created,Modified,Status,Discount
                            from `Order`
                            where OrderId = @orderId";

            return await dbHelper.QueryScalarAsync<OrderModel>(sql, new { orderId = orderId });
        }
    }
}

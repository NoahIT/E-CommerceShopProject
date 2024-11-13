using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace DAL.AdminPanel
{
    public class AdminPanelDAL : IAdminPanelDAL
    {
        private readonly IDbHelper dbHelper;

        public AdminPanelDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<int> CreateProduct(Models.Product product)
        {
            string sql = @"insert into Product(SeriesID,Model,Power,Energy,Attributes,Price)
                            values(@SeriesID,@Model,@Power,@Energy,@Attributes,@Price);
                            SELECT LAST_INSERT_ID();";

            return await dbHelper.QueryScalarAsync<int>(sql, product);
        }

        public async Task<int> CreateSeries(SeriesModel product)
        {
            string sql = @"insert into Series(CategoryId,BrandId,SeriesName,Photo,ShortDescription,Description,Characteristics,Abbr)
                            values(@CategoryId,@BrandId,@SeriesName,@Photo,@ShortDescription,@Description,@Characteristics,@Abbr);
                            SELECT LAST_INSERT_ID();";

            return await dbHelper.QueryScalarAsync<int>(sql, product);
        }

        public async Task<int> CreateCategory(string CategoryName)
        {
            string sql = @"insert into Categories(CategoryName)
                            values(@CategoryName);
                            SELECT LAST_INSERT_ID();";

            return await dbHelper.QueryScalarAsync<int>(sql, new { CategoryName = CategoryName });
        }

        public async Task<List<Models.Product>> GetAllProducts()
        {
            string sql = @"select * from Product";

            return (await dbHelper.QueryAsync<Models.Product>(sql, new { })).ToList();
        }

        public async Task UpdateProduct(Models.Product products)
        {
            string sql = @"update Product
                        set SeriesID = @SeriesID,
                        Model = @Model,
                        Power = @Power,
                        Energy = @Energy,
                        Attributes = @Attributes,
                        Price = @Price
                        where ProductId = @ProductId";

            await dbHelper.ExecuteAsync(sql, products);
        }

        public async Task DeleteProduct(int id)
        {
            string sql = @"delete from Product where ProductId = @id";

            await dbHelper.ExecuteAsync(sql, new { id = id });
        }

        public async Task CreateReklama(string Photo)
        {
            string sql = @"insert into Reklama(Photo)
                            values(@Photo)";

            await dbHelper.ExecuteAsync(sql, new {Photo = Photo});
        }

        public async Task DeleteReklama(int id)
        {
            string sql = @"delete from Reklama where idReklama = @id";

            await dbHelper.ExecuteAsync(sql, new {id = id});

        }

        public async Task<List<Models.Adds>> GetAllReklama()
        {
            string sql = @"select idReklama,Photo from Reklama";

            return (await dbHelper.QueryAsync<Models.Adds>(sql, new { })).ToList();
        }

        public async Task<List<OrderModel>> GetAllOrders()
        {
            string sql = @"select OrderId,UserId,AddressId,Created,Modified,Status,Discount from `Order`";

            return ( await dbHelper.QueryAsync<OrderModel>(sql, new { })).ToList();
        }

        public async Task<AddressModel> GetAddress(int id)
        {
            string sql = "select * from Address where AddressId = @id";

            return await dbHelper.QueryScalarAsync<AddressModel>(sql, new { id = id });
        }
        public async Task<AddressModel> UpdateOrderStatus(int id,string status)
        {
            string sql = "update `Order` set Status = @status,Modified = @mod where OrderId = @id";

            return await dbHelper.QueryScalarAsync<AddressModel>(sql, new { id = id , status = status, mod = DateTime.UtcNow});
        }

        public async Task<List<ProductTables>> GetAllProductTables()
        {
            string sql = @"select * from ProductTables";

            return (await dbHelper.QueryAsync<ProductTables>(sql, new { })).ToList();
        }

        public async Task DeleteProductTables(int id)
        {
            string sql = @"delete from ProductTables where TableId = @id";

            await dbHelper.ExecuteAsync(sql, new {id =id });
        }

        public async Task AddProductTable(ProductTables table)
        {
            string sql = @"insert into ProductTables(TableName,ModelsId,SeriesId)
                        values(@TableName,@ModelsId,@SeriesId)";

            await dbHelper.ExecuteAsync(sql, table);
        }

        public async Task<List<OrderItemModel>> GetAllOrderItems()
        {
            string sql = @"select * from OrderItem";

            return (await dbHelper.QueryAsync<OrderItemModel>(sql, new { })).ToList();
        }

        public async Task<List<BrandModel>> GetAllBrands()
        {
            string sql = @"select * from Brand";

            return (await dbHelper.QueryAsync<BrandModel>(sql, new { })).ToList();
        }

        public async Task DeleteBrand(int id)
        {
            string sql = @"delete from Brand where idBrand = @id";

            await dbHelper.ExecuteAsync(sql, new { id = id });
        }

        public async Task UpdateBrand(BrandModel model)
        {
            string sql = "update Brand set Name = @Name,Photo = @Photo,ClassName = @ClassName,Class=@Class where idBrand = @idBrand";

            await dbHelper.ExecuteAsync(sql, model);
        }

        public async Task CreateBrand(BrandModel model)
        {
            string sql = @"insert into Brand(Name,Photo,ClassName,Class)
                            values(@Name,@Photo,@ClassName,@Class)";

            await dbHelper.ExecuteAsync(sql, model);
        }

        public async Task<List<SeriesModel>> GetAllSeries()
        {
            string sql = @"select * from Series";

            return (await dbHelper.QueryAsync<SeriesModel>(sql, new { })).ToList();
        }

        public async Task DeleteSeries(int id)
        {
            string sql = @"delete from Series where idSeries = @id";

            await dbHelper.ExecuteAsync(sql, new { id = id });
        }

        public async Task UpdateSeries(SeriesModel model)
        {
            string sql = "update Series " +
                         "set " +
                         "CategoryId = @CategoryId," +
                         "BrandId = @BrandId," +
                         "SeriesName = @SeriesName," +
                         "Photo = @Photo," +
                         "ShortDescription = @ShortDescription," +
                         "Description = @Description," +
                         "Characteristics = @Characteristics," +
                         "Abbr = @Abbr " +
                         "where idSeries = @idSeries";

            await dbHelper.ExecuteAsync(sql, model);
        }

        public async Task<List<DiscountModel>> GetAllDiscount()
        {
            string sql = "select * from DiscountCode";

            return (await dbHelper.QueryAsync<DiscountModel>(sql,new {})).ToList();
        }

        public async Task DeleteDiscount(int id)
        {
            string sql = "delete from DiscountCode where idDiscount = @id";

            await dbHelper.ExecuteAsync(sql, new { id = id });
        }

        public async Task CreateDiscount(DiscountModel model)
        {
            string sql = "insert into DiscountCode(Code,Precent) " +
                         "values(@Code,@Precent)";

            await dbHelper.ExecuteAsync(sql, model);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            string sql = "select * from Categories";

            return await dbHelper.QueryAsync<CategoryViewModel>(sql, new { });
        }
    }
}

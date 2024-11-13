using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Mysqlx.Crud;

namespace DAL.AdminPanel
{
    public interface IAdminPanelDAL
    {
        Task<int> CreateProduct(Models.Product product);
        Task<int> CreateSeries(Models.SeriesModel product);
        Task<int> CreateCategory(string CategoryName);
        Task<List<Models.Product>> GetAllProducts();
        Task UpdateProduct(Models.Product products);
        Task DeleteProduct(int id);
        Task CreateReklama(string Photo);
        Task DeleteReklama(int id);
        Task<List<Models.Adds>> GetAllReklama();
        Task<List<OrderModel>> GetAllOrders();
        Task<AddressModel> GetAddress(int id);
        Task<AddressModel> UpdateOrderStatus(int id, string status);
        Task<List<ProductTables>> GetAllProductTables();
        Task DeleteProductTables(int id);
        Task AddProductTable(ProductTables table);
        Task<List<OrderItemModel>> GetAllOrderItems();
        Task<List<BrandModel>> GetAllBrands();
        Task DeleteBrand(int id);
        Task UpdateBrand(BrandModel model);
        Task CreateBrand(BrandModel model);

        Task<List<SeriesModel>> GetAllSeries();
        Task DeleteSeries(int id);
        Task UpdateSeries(SeriesModel model);

        Task<List<DiscountModel>> GetAllDiscount();
        Task DeleteDiscount(int id);
        Task CreateDiscount(DiscountModel model);

        Task<IEnumerable<CategoryViewModel>> GetAllCategories();

    }
}

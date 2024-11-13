using DAL.Models;

namespace DAL.Interfaces
{
    public interface IProductDAL
    {
        Task<List<Models.Product>> GetProductsByIds(List<int> productsIds);
        Task<List<ProductModelHome>> GetProductsForHomePage();
    }
}

using DAL.Models;

namespace BL.Products
{
    public interface IProducts
    {
        Task<List<ProductTables>> GetProductTables(int id);

        Task<List<ProductModelHome>> GetProductsForHomePage();
        Task<IEnumerable<ProductCardModel>> GetProductsForSearch(string searchText, int page, int pageSize);
        Task<List<ProductCardModel>> GetProductsForSearch(string searchText);
        Task<int> CountProductsForSearch(string searchText);

        Task<IEnumerable<QuickSearchModel>> GetQuickSearchForSearch(string searchText);
        
    }
}

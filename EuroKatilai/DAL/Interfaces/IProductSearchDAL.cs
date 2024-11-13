using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductSearchDAL
    {
        Task<IEnumerable<ProductCardModel>> SearchProducts(string searchText);
        Task<IEnumerable<ProductCardModel>> GetProductsForSearch(string searchText, int page, int pageSize);
        Task<int> CountProductsForSearch(string searchText);

        Task<IEnumerable<QuickSearchModel>> QuickSearch(string searchQuery);
    }
}

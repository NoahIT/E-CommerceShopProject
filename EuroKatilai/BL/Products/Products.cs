using DAL.Interfaces;
using DAL.Models;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Misc;

namespace BL.Products
{
    public class Products : IProducts
    {
        private readonly IProductDAL productDAL;
        private readonly IProductTablesDAL tablesDAL;
        private readonly IProductSearchDAL searchDAL;

        public Products(IProductDAL productDal, IProductTablesDAL tablesDal, IProductSearchDAL searchDal)
        {
            productDAL = productDal;
            tablesDAL = tablesDal;
            searchDAL = searchDal;
        }

        public async Task<List<ProductTables>> GetProductTables(int id)
        {
            var tables = await tablesDAL.GetTablesBySeriesId(id);

            foreach (var table in tables)
            {
                table.Models = await productDAL.GetProductsByIds(table.ModelsIntList);

                foreach (var x in table.Models)
                {
                    x.DicAttributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(x.Attributes) ?? new Dictionary<string, string>();
                }
            }

            return tables ?? new List<ProductTables>();
        }

        public async Task<List<ProductModelHome>> GetProductsForHomePage()
        {
            var products = await productDAL.GetProductsForHomePage();


            return products.OrderBy(x=> Guid.NewGuid()).Take(24).ToList();
        }

        public async Task<List<ProductCardModel>> GetProductsForSearch(string search)
        {
            var products = await searchDAL.SearchProducts(search);

            return products.ToList();
        }

        public async Task<int> CountProductsForSearch(string searchText)
        {
            return await searchDAL.CountProductsForSearch(searchText);
        }


        public async Task<IEnumerable<ProductCardModel>> GetProductsForSearch(string searchText, int page, int pageSize)
        {
            return await searchDAL.GetProductsForSearch(searchText,page,pageSize);
        }


        public async Task<IEnumerable<QuickSearchModel>> GetQuickSearchForSearch(string searchText)
        {
            return await searchDAL.QuickSearch(searchText);
        }
    }
}

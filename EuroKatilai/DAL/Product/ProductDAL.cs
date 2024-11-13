using DAL.Interfaces;
using DAL.Models;

namespace DAL.Product
{
    public class ProductDAL : IProductDAL
    {
        private readonly IDbHelper dbHelper;

        public ProductDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<List<Models.Product>> GetProductsByIds(List<int> productsIds)
        {
            string sql = "select ProductId,SeriesID,Model,Power,Energy,Attributes,Price from Product where ProductId = @id";
            var products = new List<Models.Product>();

            foreach (var x in productsIds)
            {
                var value = await dbHelper.QueryScalarAsync<Models.Product>(sql, new { id = x });
                products.Add(value);
            }

            return products ?? new List<Models.Product>();
        }

        public async Task<List<ProductModelHome>> GetProductsForHomePage()
        {
            string sql = @"select s.idSeries,s.Photo,p.Model,b.Name,p.Price,s.SeriesName,p.ProductId
                            from Product p
                            join Series s ON s.idSeries = p.SeriesID
                            join Brand b ON b.idBrand = s.BrandId";

            var products = await dbHelper.QueryAsync<ProductModelHome>(sql, new { });

            return products.ToList();
        }
    }
}

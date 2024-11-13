using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Product
{
    public class ProductSearchDAL : IProductSearchDAL
    {
        private readonly IDbHelper dbHelper;

        public ProductSearchDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<IEnumerable<ProductCardModel>> SearchProducts(string searchText)
        {
            var query = @"SELECT p.ProductId, s.idSeries, p.Model as ModelName, s.SeriesName, s.Photo, p.Price, b.Name as BrandName
                      FROM Product p
                      JOIN Series s ON p.SeriesID = s.idSeries
                      JOIN Brand b ON s.BrandId = b.idBrand
                      WHERE LOWER(p.Model) LIKE LOWER(@SearchText) OR LOWER(s.SeriesName) LIKE LOWER(@SearchText) OR LOWER(b.Name) LIKE LOWER(@SearchText)";

           return (await dbHelper.QueryAsync<ProductCardModel>(query, new { SearchText = $"%{searchText}%" })).ToList();
        }
        public async Task<int> CountProductsForSearch(string searchText)
        {
            var countQuery = @"SELECT COUNT(*)
                       FROM Product p
                       JOIN Series s ON p.SeriesID = s.idSeries
                       JOIN Brand b ON s.BrandId = b.idBrand
                       WHERE LOWER(p.Model) LIKE LOWER(@SearchText) 
                         OR LOWER(s.SeriesName) LIKE LOWER(@SearchText) 
                         OR LOWER(b.Name) LIKE LOWER(@SearchText)";

            return await dbHelper.QueryScalarAsync<int>(countQuery, new { SearchText = $"%{searchText}%" });
        }

        public async Task<IEnumerable<QuickSearchModel>> QuickSearch(string searchQuery)
        {
            var sql = @"
                        SELECT 
                            s.idSeries as SeriesId, 
                            p.Model as ModelName, 
                            s.SeriesName, 
                            s.Photo, 
                            p.Price, 
                            b.Name as BrandName
                        FROM Product p
                        JOIN Series s ON p.SeriesID = s.idSeries
                        JOIN Brand b ON s.BrandId = b.idBrand
                        WHERE 
                           LOWER(CONCAT(b.Name, ' ', s.SeriesName, ' ', p.Model)) LIKE LOWER(@SearchText) OR
                           LOWER(CONCAT(s.SeriesName, ' ', b.Name, ' ', p.Model)) LIKE LOWER(@SearchText) OR
                           LOWER(CONCAT(p.Model, ' ', s.SeriesName, ' ', b.Name)) LIKE LOWER(@SearchText) OR
                           LOWER(CONCAT(b.Name, ' ', p.Model, ' ', s.SeriesName)) LIKE LOWER(@SearchText);";

            return await dbHelper.QueryAsync<QuickSearchModel>(sql, new { SearchText = $"%{searchQuery}%" });

        }

        public async Task<IEnumerable<ProductCardModel>> GetProductsForSearch(string searchText, int page, int pageSize)
        {
            var pageQuery = @"SELECT p.ProductId, s.idSeries, p.Model as ModelName, s.SeriesName, s.Photo, p.Price, b.Name as BrandName
                  FROM Product p
                  JOIN Series s ON p.SeriesID = s.idSeries
                  JOIN Brand b ON s.BrandId = b.idBrand
                  WHERE LOWER(p.Model) LIKE LOWER(@SearchText) 
                    OR LOWER(s.SeriesName) LIKE LOWER(@SearchText) 
                    OR LOWER(b.Name) LIKE LOWER(@SearchText)
                  LIMIT @PageRows OFFSET @OffsetRows";

            int offsetRows = (page - 1) * pageSize; // Calculate the offset based on the page number

            // Now you can execute the query with Dapper using the adjusted parameters
            return await dbHelper.QueryAsync<ProductCardModel>(
                pageQuery,
                new
                {
                    SearchText = $"%{searchText}%",
                    PageRows = pageSize,
                    OffsetRows = offsetRows
                }
            );
        }



    }
}

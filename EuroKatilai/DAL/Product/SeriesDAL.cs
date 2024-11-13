using DAL.Interfaces;
using DAL.Models;

namespace DAL.Product
{
    public class SeriesDAL : ISeriesDAL
    {
        private readonly IDbHelper dbHelper;

        public SeriesDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<List<SeriesPageModel>> getSeriesByCategory(string abbr)
        {
            string sql = @"SELECT s.idSeries, s.SeriesName, s.BrandId, s.Photo, s.ShortDescription, s.CategoryId, c.CategoryName
                            FROM Series s
                            JOIN Categories c ON s.CategoryId = c.idCategories
                            WHERE s.Abbr = @abbr";

            var x = await dbHelper.QueryAsync<SeriesPageModel>(sql, new { abbr });


            return x.ToList();
        }

        public async Task<List<SeriesPageModel>> getSeriesByBrandId(int id)
        {
            string sql = @"SELECT s.idSeries, s.SeriesName, s.BrandId, s.Photo, s.ShortDescription, s.CategoryId, c.CategoryName
                            FROM Series s
                            JOIN Categories c ON s.CategoryId = c.idCategories
                            WHERE s.BrandId = @id";

            var x = await dbHelper.QueryAsync<SeriesPageModel>(sql, new { id });


            return x.ToList();
        }

        public async Task<ProductViewSeriesModel> GetSeriesBySeriesId(int seriesId)
        {
            string sql = @"select idSeries,SeriesName,Description,Characteristics,BrandId,Photo from Series where idSeries = @seriesId";

            return await dbHelper.QueryScalarAsync<ProductViewSeriesModel>(sql, new { seriesId });
        }
    }
}

using DAL.Models;

namespace DAL.Interfaces
{
    public interface ISeriesDAL
    {
        Task<List<SeriesPageModel>> getSeriesByCategory(string category);
        Task<List<SeriesPageModel>> getSeriesByBrandId(int id);
        Task<ProductViewSeriesModel> GetSeriesBySeriesId(int seriesId);

    }
}

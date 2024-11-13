using DAL.Models;

namespace BL.Series
{
    public interface ISeries
    {
        Task<List<List<SeriesPageModel>>> GetSeriesByCategoryAsync(string category);
        Task<List<List<SeriesPageModel>>> GetSeriesByBrandId(int id);
        Task<ProductViewSeriesModel?> GetSeriesBySeriesId(int seriesId);
    }
}

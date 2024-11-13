using DAL.Interfaces;
using DAL.Models;

namespace BL.Series
{
    public class Series : ISeries
    {
        private readonly ISeriesDAL seriesDAL;

        public Series(ISeriesDAL seriesDal)
        {
            seriesDAL = seriesDal;
        }

        public async Task<List<List<SeriesPageModel>>> GetSeriesByCategoryAsync(string category)
        {
            return ScaleList(await seriesDAL.getSeriesByCategory(category));
        }

        public async Task<List<List<SeriesPageModel>>> GetSeriesByBrandId(int id)
        {
            return ScaleList(await seriesDAL.getSeriesByBrandId(id));
        }

        public async Task<ProductViewSeriesModel?> GetSeriesBySeriesId(int seriesId)
        {
            return await seriesDAL.GetSeriesBySeriesId(seriesId);
        }

        private List<List<SeriesPageModel>> ScaleList(List<SeriesPageModel> all)
        {
            var groupedByType = all
                .GroupBy(model => model.CategoryName)
                .Select(group => group.ToList())
                .ToList();

            return groupedByType;
        }
    }
}

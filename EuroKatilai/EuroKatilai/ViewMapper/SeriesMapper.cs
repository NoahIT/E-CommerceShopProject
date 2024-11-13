using DAL.Models;
using EuroKatilai.ViewModels;

namespace EuroKatilai.ViewMapper
{
    public class SeriesMapper
    {
        public static SeriesViewModel SeriesPageModelToSeriesViewModel(List<List<SeriesPageModel>> x)
        {
            return new SeriesViewModel() { series = x };
        }
    }
}

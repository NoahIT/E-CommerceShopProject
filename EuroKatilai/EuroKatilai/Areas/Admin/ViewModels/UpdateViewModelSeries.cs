using DAL.Models;

namespace EuroKatilai.Areas.Admin.ViewModels
{
    public class UpdateViewModelSeries
    {
        public List<SeriesModel> Series { get; set; } = new List<SeriesModel>();
        public SeriesModel CurrentSeries { get; set; } = new SeriesModel();
    }
}

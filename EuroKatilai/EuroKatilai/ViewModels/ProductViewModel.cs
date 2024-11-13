using DAL.Models;

namespace EuroKatilai.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewSeriesModel SeriesModel { get; set; }
        public List<ProductTables> Tables { get; set; }
    }
}

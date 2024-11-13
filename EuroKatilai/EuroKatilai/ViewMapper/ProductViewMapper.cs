using System.Security.Cryptography.X509Certificates;
using DAL.Models;
using EuroKatilai.ViewModels;

namespace EuroKatilai.ViewMapper
{
    public class ProductViewMapper
    {

        public static ProductViewModel MapperProductViewModel(List<ProductTables> tables, ProductViewSeriesModel seriesModel)
        {
            return new ProductViewModel()
            {
                SeriesModel = seriesModel,
                Tables = tables
            };
        }
    }
}

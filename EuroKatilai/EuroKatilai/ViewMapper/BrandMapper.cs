using DAL.Models;
using EuroKatilai.ViewModels;

namespace EuroKatilai.ViewMapper
{
    public class BrandMapper
    {
        public static BrandViewModel MapperToBrandViewModel(List<BrandModel> brandmodels)
        {
            var brands = new BrandViewModel();
            brands.Brands = brandmodels;

            return brands;
        }
    }
}

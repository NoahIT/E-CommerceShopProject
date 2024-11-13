using BL.Brand;
using DAL.Models;

namespace EuroKatilai.Areas.Admin.ViewModels
{
    public class UpdateViewModelBrand
    {
        public List<BrandModel> Brands { get; set; } = new List<BrandModel>();
        public BrandModel CurrentBrand { get; set; } = new BrandModel();
    }
}

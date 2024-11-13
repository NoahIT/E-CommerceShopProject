using BL.Auth;
using BL.Brand;
using EuroKatilai.ViewMapper;
using EuroKatilai.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Controllers
{
    public class BrandViewController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IBrand brand;

        public BrandViewController(ICurrentUser current,IBrand brand)
        {
            this.currentUser = current;
            this.brand = brand;
        }

        [HttpGet]
        [Route("brands/{_class}")]
        public async Task<IActionResult> Index(string _class)
        {
            var brands = await brand.GetBrandsByClass(_class);

            if (brands.Count > 0)
            {
                return View("Index", BrandMapper.MapperToBrandViewModel(brands) ?? new BrandViewModel()); 
            }

            return View("Error404");
        }
    }
}

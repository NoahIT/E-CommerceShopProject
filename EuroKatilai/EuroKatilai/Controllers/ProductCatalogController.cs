using BL.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Controllers
{
    public class ProductCatalogController : Controller
    {
        private readonly ICurrentUser currentUser;

        public ProductCatalogController(ICurrentUser current)
        {
            this.currentUser = current;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

using EuroKatilai.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SiteAuthorize(RequireAdmin: true)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

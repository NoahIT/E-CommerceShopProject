using BL.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ICurrentUser currentUser;

        public AboutUsController(ICurrentUser current)
        {
            this.currentUser = current;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

using BL.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ICurrentUser currentUser;

        public ContactsController(ICurrentUser current)
        {
            this.currentUser = current;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

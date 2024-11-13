using BL.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly ICurrentUser currentUser;
        public MainMenuViewComponent(ICurrentUser current)
        {
            this.currentUser = current;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            bool isLoggedIn = await currentUser.IsLoggedIn();

            bool isAdmin = currentUser.IsAdmin();

            ViewBag.isAdmin = isAdmin;

            return View("Index", isLoggedIn );
        }


    }
}

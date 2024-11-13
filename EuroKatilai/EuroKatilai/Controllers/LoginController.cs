using BL.Auth;
using BL.Exceptions;
using EuroKatilai.Middleware;
using EuroKatilai.ViewMapper;
using EuroKatilai.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Controllers
{
    [SiteNotAuthorize()]
    public class LoginController : Controller
    {
        private readonly IAuth authBl;

        public LoginController(IAuth authBL)
        {
            this.authBl = authBL;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (AuthorizationException e)
                {
                    ModelState.AddModelError("Email", "Email arba slaptažodis neteisingi");
                }

            }

            return View("Index", model);
        }
    }
}

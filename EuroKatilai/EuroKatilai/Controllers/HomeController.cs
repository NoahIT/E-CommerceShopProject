using EuroKatilai.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BL.Auth;
using BL.Email;
using BL.Products;
using EuroKatilai.ViewModels;
using BL.Adds;
using DAL.Models;

namespace EuroKatilai.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProducts products;
        private readonly IEmailSender email;
        private readonly IReklama reklama1;

        public HomeController(ICurrentUser current, IProducts products, IEmailSender email, IReklama reklama)
        {
            this.currentUser = current;
            this.products = products;
            this.email = email;
            this.reklama1 = reklama;
        }

        public async Task<IActionResult> Index()
        {
            var randomProducts = await this.products.GetProductsForHomePage();

            var reklama = await reklama1.GetAllPathsToPhotos();

            return View("Index", new HomeViewModel() { products = randomProducts, reklamos = reklama});
        }

        public IActionResult Privacy()
        {
            return View("Index");
        }


        [Route("/logout")]
        public IActionResult Logout()
        {
            currentUser.Logout();

            currentUser.DeleteNotUsebaleSessions();

            return Redirect("/");
        }


        [Route("Home/Error")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode.Value == 404 || statusCode.Value == 500)
                {
                    // Вернуть специфичное для кода состояния представление
                    return View($"Error404");
                }
            }

            // Вернуть общее представление ошибки
            return View();
        }

        [HttpPost]
        [Route("/send-email")]
        public async Task<IActionResult> SendChatBot(ChatBotModel model)
        {
            await email.SendEmail(new ChatBotModel()
            {
                Name = model.Name,
                Email = "info@eurokatilai.lt",
                Message = $"Naudotojo email: {model.Email} \nMessage: {model.Message}",
                Subject = $"Jums naujas pranešimas nuo {model.Name}.",
            });


            model.Subject = "Automatinis pranešimas";
            model.Message = "Jus sekmingai pateikete užklausa, greitu metu susisieksime su jumis";
            await email.SendEmail(model);


            return Redirect("/");
        }
    }
}
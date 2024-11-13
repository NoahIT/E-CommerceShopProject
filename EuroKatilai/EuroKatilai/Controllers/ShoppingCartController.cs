using BL.Auth;
using BL.Cart;
using BL.Models;
using DAL.Models;
using EuroKatilai.ViewMapper;
using EuroKatilai.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICart cart;

        public ShoppingCartController(ICart cart, ICurrentUser currentUser)
        {
            this.cart = cart;
        }


        [HttpGet]
        [Route("/cart")]
        public async Task<IActionResult> Index()
        {
            var model = await cart.GetCurrentUserCart();

            if (model.Items.Count<=0 )
            {
                return View("IndexEmpty");
            }

            return View(model);
        }

        [HttpPost]
        [Route("/cart/add")]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> Add(int productId)
        {
            await cart.AddCurrentUserCartProduct(productId);
            return Redirect("/cart");
        }

        [HttpPost]
        [Route("/cart/update")]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> update(CartUpdateViewModel model)
        {
            await cart.UpdateCurrentUserCartProduct(model.Productid, model.ProductCount);
            return Redirect("/cart");
        }

        [HttpPost]
        [Route("/cart/discount")]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Discount(string code)
        {
            var model = await cart.GetCurrentUserCart();

            if (String.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("Discount", "Įveskite nuolaidos koda");

                return View("Index", model);
            }

            var val = await cart.UpdateCartDiscountId(model.Cart.CartId ?? 0, code);

            if (val)
            {
                return Redirect("/cart");   
            }

            ModelState.AddModelError("Discount", "Toks nuolaidos kodas neegzistuoja");

            return View("Index",model);
        }

        [HttpPost]
        [Route("/cart/delete-discount")]
        public async Task<IActionResult> DeleteDiscount()
        {
            var model = await cart.GetCurrentUserCart();

           await cart.DeleteDiscountId(model.Cart.CartId ?? 0);

            return Redirect("/cart");
        }
    }
}

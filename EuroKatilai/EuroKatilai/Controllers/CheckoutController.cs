using BL.Auth;
using BL.Cart;
using BL.Profile;
using DAL.Models;
using EuroKatilai.ViewMapper;
using EuroKatilai.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICart cart;
        private readonly IAddress address;

        private readonly ICurrentUser currentUser;

        public CheckoutController(ICart cart, IAddress address, ICurrentUser currentUser)
        {
            this.cart = cart;
            this.address = address;
            this.currentUser = currentUser;
        }

        [HttpGet]
        [Route("/checkout/address")]
        public async Task<IActionResult> Address()
        {
            var userCart = await this.cart.GetCurrentUserCart();
            userCart.Cart.AddressId = (await this.address.GetAddressByUserId(await this.currentUser.GetCurrentUserId())).AddressId;
            ViewBag.Cart = userCart;


            AddressViewModel addressViewModel = new AddressViewModel();
            if (userCart.Cart.AddressId != null)
            {
                AddressModel addressModel = await address.GetAddress((int)userCart.Cart.AddressId);
                addressViewModel = ProfileMapper.MapAddressModelToAddressViewModel(addressModel);
            }

            return View(addressViewModel);
        }

        [HttpPost]
        [Route("/checkout/address")]
        public async Task<IActionResult> AddressSave(AddressViewModel model)
        {
            var userCart = await this.cart.GetCurrentUserCart(); 
            //userCart.Cart.AddressId = (await this.address.GetAddressByUserId(await this.currentUser.GetCurrentUserId())).AddressId;
            ViewBag.Cart = userCart;
            if (ModelState.IsValid)
            {
                AddressModel addressModel = ProfileMapper.MapAddressViewModelToAddressModel(model);
                //addressModel.UserId = await currentUser.GetCurrentUserId() ?? 0;
                addressModel.AddressId = userCart.Cart.AddressId;

                int addressId = await address.Save(addressModel);

                userCart.Cart.AddressId = addressId;
                await this.cart.UpdateCurrentUserCart(userCart.Cart);
                return this.Redirect("/checkout/billing");
            }

            return View("Address", model);
        }

        [HttpGet]
        [Route("/checkout/billing")]
        public async Task<IActionResult> Billing()
        {
            return View("PaymentView");
        }


        [HttpPost]
        [Route("/checkout/billing")]
        public async Task<IActionResult> BillingSave()
        {
            var response = await cart.PlaceOrder();

            if (response)
            {
                return View("SuccessfullIndex");     
            }
            else
            {
                return View("NotSuccess");
            }
        }

    }
}

using EuroKatilai.Middleware;
using EuroKatilai.Service;
using EuroKatilai.ViewMapper;
using EuroKatilai.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using BL.Auth;
using BL.Cart;
using BL.Models;
using BL.Profile;
using DAL.Models;
using EuroKatilai.Views.Profile;

namespace EuroKatilai.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        public readonly ICurrentUser currentUser;
        public readonly IProfile profile;
        public readonly IEncrypt encrypt;
        public readonly IDbSession dbSession;
        public readonly IOrder order;

        public ProfileController(ICurrentUser user, IProfile profile, IEncrypt encrypt, IDbSession dbSession, IOrder order)
        {
            this.currentUser = user;
            this.profile = profile;
            this.encrypt = encrypt;
            this.dbSession = dbSession;
            this.order = order;
        }

        [HttpGet]
        [Route("/profile")]
        public async Task<IActionResult> Index(string tab = "my-profile")
        {
            switch (tab)
            {
                case "my-profile":
                    var profiles = await currentUser.GetProfiles();
                    return View(profiles != null
                        ? ProfileMapper.MapProfileModelToProfileViewModel(profiles)
                        : new ProfileViewModel());
                    break;
                case "my-orders":
                    var orders = await order.GetOrderModels((int)await currentUser.GetCurrentUserId());
                    return View("IndexMyOrders", orders ?? new List<OrderModel>());
                    break;
                case "change-password":
                    return View("IndexChangePassword", new ChangePasswordModel());
                    break;
                case "remove-profile":
                    return View("IndexRemoveMyProfile");
                    break;
                default:
                    return View("Error404");
            }

        }

        [HttpPost]
        [Route("/profile/update")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(ProfileViewModel model)
        {
            int? userid = await currentUser.GetCurrentUserId();
            if (userid == null)
                throw new Exception("Пользователь не найден");

            var profiles = await profile.Get((int)userid);


                ProfileModel profileModel = ProfileMapper.MapProfileViewModelToProfileModel(model);
                profileModel.UserId = (int)userid;
                AddressModel addressModel = ProfileMapper.MapProfileViewModelToAddressModel(model);
                await profile.AddOrUpdate(profileModel, addressModel);
                return Redirect("/profile");

        }

        [HttpPost]
        [Route("/profile/change-password")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NewPassword != model.NewPassword2)
                {
                    TempData["ErrorMessage"] = "Įvedete skirtingus slaptažodžius";
                    return Redirect("/profile?tab=change-password");
                }

                var curr = await currentUser.GetCurrentUserPassword();

                if (curr.Password != encrypt.HashPassword(model.OldPassword, curr.Salt))
                {
                    TempData["ErrorMessage"] = "Senas slaptažodis nera teisingas";
                    return Redirect("/profile?tab=change-password");
                }

                await dbSession.UpdatePassword(encrypt.HashPassword(model.NewPassword, curr.Salt));

                TempData["ErrorMessage"] = "";
                TempData["SuccessMessage"] = "Slaptažodis sėkmingai pakeistas.";
                return Redirect("/profile?tab=change-password");
            }
            else
            {
                return View("IndexChangePassword",new ChangePasswordModel());
            }
        }

        [HttpPost]
        [Route("/profile/delete-account")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await currentUser.GetCurrentUserId();
            currentUser.Logout();

            await currentUser.DeleteAccount((int)user);

           return Redirect("/");
        }


        [HttpGet]
        [Route("/profile/logout")]
        public IActionResult Logout()
        {
            currentUser.Logout();

            return Redirect("/");
        }

        [HttpPost]
        [Route("/profile/order-details")]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var orderModel = await this.order.GetOrder(orderId);
            return View("OrderDetailsIndex", orderModel ?? new UserOrderModel());
        }


        [HttpGet]
        [Route("/profile/order-details")]
        public IActionResult OrderDetailsErr()
        { 
            return View("Error404");
        }
    }
}

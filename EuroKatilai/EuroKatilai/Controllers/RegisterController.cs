using BL.Auth;
using BL.Exceptions;
using BL.Profile;
using DAL.Models;
using EuroKatilai.Middleware;
using EuroKatilai.ViewMapper;
using EuroKatilai.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Controllers
{
    [SiteNotAuthorize()]
    public class RegisterController : Controller
    {
        private readonly IAuth authBl;
        private readonly IProfile profile;

        public RegisterController(IAuth authBL, IProfile profile)
        {
            this.authBl = authBL;
            this.profile = profile;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost]
        [Route("/register")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = await authBl.Register(AuthMapper.MapRegisterViewModelToUserModel(model));

                    var address = new AddressModel()
                    {
                        Email = model.Email,
                        City = model.City,
                        Country = model.Country,
                        Region = model.Region,
                        UserId = id,
                        ZipCode = model.ZipCode,
                        Street = model.Street,
                        House = model.House,
                        Appartment = model.Appartment,
                        Code = model.Code,
                        PVMCode = model.PVMCode,
                        CompanyName = model.CompanyName,
                        RecieverName = model.RecieverName,
                        RecieverSurname = model.RecieverSurname,
                        Phone = model.Phone,
                        Delivery = model.Delivery,
                        Status = model.Status,
                        Created = DateTime.UtcNow,
                        Modified = DateTime.UtcNow
                    };

                    await profile.AddOrUpdate(new ProfileModel()
                    {
                        UserId = id
                    }, address);

                    return Redirect("/");
                }
                catch (DuplicateEmailException)
                {
                    ModelState.TryAddModelError("Email", "Email jau egzistuoja");
                }
            }

            return View("Index", model);
        }
    }
}

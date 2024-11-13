using BL.Email;
using DAL.AdminPanel;
using DAL.Models;
using EuroKatilai.Areas.Admin.ViewModels;
using EuroKatilai.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace EuroKatilai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SiteAuthorize(RequireAdmin: true)]
    public class UzsakymaiController : Controller
    {
        private readonly IAdminPanelDAL admin;
        private readonly IEmailSender email;

        public UzsakymaiController(IAdminPanelDAL admin, IEmailSender email)
        {
            this.admin = admin;
            this.email = email;
        }

        [HttpGet]
        [Route("/admin/uzsakymai")]
        public async Task<IActionResult> Index(int addressId = 0)
        {
            var orders = await admin.GetAllOrders();

            orders = orders.OrderByDescending(x => x.Modified).ToList();

            var addressModel = await admin.GetAddress(addressId);
            string address = "";

            if (addressModel != null)
            {
                address = $"{addressModel.Country} {addressModel.Region} {addressModel.City} " +
                          $"{addressModel.Street}. {addressModel.House} butas:{addressModel.Appartment}, {addressModel.CompanyName} " +
                          $"{addressModel.PVMCode} {addressModel.Code} Gavėjo vardas pavarde: {addressModel.RecieverName} {addressModel.RecieverSurname}";
            }

            return View("Index", new UzsakymaiViewModel(){orders = orders, address = address});
        }

        [HttpPost]
        [Route("/admin/uzsakymai/update-status")]
        public async Task<IActionResult> UpdateStatus(int OrderId, string status, int addressId)
        {
            var orders = await admin.UpdateOrderStatus(OrderId, status);

            var address = await admin.GetAddress(addressId);

            ChatBotModel model = new ChatBotModel()
            {
                Email = address.Email,
                Message = $"Jūsų užsakymui:{OrderId}, buvo pakeistas statusas: \"{status}\"",
                Subject = $"Informacija dėl {OrderId} užsakymo.",
                Name = address.RecieverName
            };

            await email.SendEmail(model);

            return Redirect("/admin/uzsakymai");
        }

        [HttpPost]
        [Route("/admin/uzsakymai/find-address")]
        public async Task<IActionResult> ShowAddress(int addressId)
        {
            return Redirect($"/admin/uzsakymai?addressId={addressId}");
        }
    }
}

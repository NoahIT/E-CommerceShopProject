using DAL.AdminPanel;
using EuroKatilai.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SiteAuthorize(RequireAdmin: true)]
    public class OrderItemsController : Controller
    {
        private readonly IAdminPanelDAL admin;

        public OrderItemsController(IAdminPanelDAL admin)
        {
            this.admin = admin;
        }

        [HttpGet]
        [Route("/admin/orderItems")]
        public async Task<IActionResult> Index()
        {
            var orderItems = await admin.GetAllOrderItems();

            return View("Index", orderItems);
        }
    }
}

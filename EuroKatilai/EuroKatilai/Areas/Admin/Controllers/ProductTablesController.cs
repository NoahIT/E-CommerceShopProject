using DAL.AdminPanel;
using DAL.Models;
using EuroKatilai.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SiteAuthorize(RequireAdmin: true)]
    public class ProductTablesController : Controller
    {
        private readonly IAdminPanelDAL admin;

        public ProductTablesController(IAdminPanelDAL admin)
        {
            this.admin = admin;
        }

        [HttpGet]
        [Route("/admin/productTables")]
        public async Task<IActionResult> Index()
        {
            var tables = await admin.GetAllProductTables();
            return View("Index", tables);
        }

        [HttpPost]
        [Route("/admin/productTables/add")]
        public async Task<IActionResult> Add(ProductTables table)
        {
            await admin.AddProductTable(table);

            return Redirect("/admin/productTables");
        }

        [HttpPost]
        [Route("/admin/productTables/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await admin.DeleteProductTables(id);

            return Redirect("/admin/productTables");
        }
    }
}

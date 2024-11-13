using DAL.AdminPanel;
using EuroKatilai.Areas.Admin.BL;
using EuroKatilai.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SiteAuthorize(RequireAdmin: true)]
    public class ReklamosController : Controller
    {
        private readonly IAdminPanelDAL admin;
        private readonly IWebPhoto photos;

        public ReklamosController(IAdminPanelDAL admin, IWebPhoto photo)
        {
            this.admin = admin;
            this.photos = photo;
        }

        [HttpGet]
        [Route("/admin/reklamos")]
        public async Task<IActionResult> Index()
        {
            var reklama = await admin.GetAllReklama();

            return View("Index", reklama);
        }

        [HttpPost]
        [Route("/admin/reklama/delete-reklama")]
        public async Task<IActionResult> Delete(int idReklama)
        {
            await admin.DeleteReklama(idReklama);

            return Redirect("/admin/reklamos");
        }

        [HttpPost]
        [Route("/admin/reklama/add-new-promo-photo")]
        public async Task<IActionResult> Add(IFormFile photo)
        {
            if (photo == null)
            {
                return Redirect("/admin/reklamos");
            }

            var path = await photos.UploadPhotoAndReturnFilePath(photo);

            await admin.CreateReklama((string)path);

            return Redirect("/admin/reklamos");
        }


    }
}

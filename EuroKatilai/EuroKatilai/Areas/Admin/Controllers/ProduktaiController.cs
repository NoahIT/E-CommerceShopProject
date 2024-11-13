using DAL.AdminPanel;
using DAL.Models;
using EuroKatilai.Areas.Admin.BL;
using EuroKatilai.Areas.Admin.ViewModels;
using EuroKatilai.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SiteAuthorize(RequireAdmin: true)]
    public class ProduktaiController : Controller
    {
        private readonly IWebPhoto uploader;
        private readonly IAdminPanelDAL admin;

        public ProduktaiController(IWebPhoto uploader, IAdminPanelDAL admin)
        {
            this.uploader = uploader;
            this.admin = admin;
        }

        [HttpGet]
        [Route("/admin/products/create-new-product")]
        public async Task<IActionResult> Index()
        {
            var categories = await admin.GetAllCategories();

            return View("CreateProductIndex", categories);
        }

        [HttpPost]
        [Route("/admin/products/create-product")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            int id = await admin.CreateProduct(product);

            return Redirect("/admin/products/create-new-product");
        }

        [HttpPost]
        [Route("/admin/products/create-series")]
        public async Task<IActionResult> CreateProduct(IFormFile photos, SeriesModel model)
        {
            string? path = await uploader.UploadPhotoAndReturnFilePath(photos);

            if (path!=null)
            {
                model.Photo = path.Replace('/','\\');
                int id = await admin.CreateSeries(model);
            }

            return Redirect("/admin/products/create-new-product");
        }

        [HttpPost]
        [Route("/admin/products/create-category")]
        public async Task<IActionResult> CreateCategory(string CategoryName)
        {
            var id = await admin.CreateCategory(CategoryName);

            return Redirect("/admin/products/create-new-product");
        }

        [HttpGet]
        [Route("/admin/products/update-existing-product")]
        public async Task<IActionResult> UpdateProduct()
        {
            var products = await admin.GetAllProducts();
            return View("UpdateExistingProductIndex", new UpdateViewModel(){Products = products});
        }

        [HttpPost]
        [Route("/admin/products/update-existing-product")]
        public async Task<IActionResult> UpdateProduct1(Product product)
        {
            var products = await admin.GetAllProducts();

            return View("UpdateExistingProductIndex", new UpdateViewModel() { Products = products, CurrentProduct = product});
        }

        [HttpPost]
        [Route("/admin/products/update-product")]
        public async Task<IActionResult> UpdateProduct2(Product product)
        {
            await admin.UpdateProduct(product);

            return Redirect("/admin/products/update-existing-product");
        }

        [HttpPost]
        [Route("/admin/products/delete-existing-product")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await admin.DeleteProduct(id);

            return Redirect("/admin/products/update-existing-product");
        }

        [HttpPost]
        [Route("/admin/products/delete-brand")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            await admin.DeleteBrand(id);

            return Redirect("/admin/produktai/brands");
        }

        [HttpPost]
        [Route("/admin/products/update-brand")]
        public async Task<IActionResult> UpdateBrand(BrandModel model, IFormFile? newPhoto)
        {
            if (newPhoto != null)
            {
               var newPAth = await uploader.UploadPhotoAndReturnFilePath(newPhoto);
               model.Photo = newPAth;
            }

            await admin.UpdateBrand(model);

            return Redirect("/admin/produktai/brands");
        }

        [HttpGet]
        [Route("/admin/produktai/brands")]
        public async Task<IActionResult> Brands()
        {
            var brands = await admin.GetAllBrands();

            return View("UpdateExistringBrand",new UpdateViewModelBrand(){Brands = brands});
        }

        [HttpPost]
        [Route("/admin/produktai/brands")]
        public async Task<IActionResult> Brands(BrandModel model)
        {
            var brands = await admin.GetAllBrands();

            return View("UpdateExistringBrand", new UpdateViewModelBrand() { Brands = brands, CurrentBrand = model });
        }

        [HttpPost]
        [Route("/admin/products/create-brand")]
        public async Task<IActionResult> CreateBrand(BrandModel model,IFormFile photos)
        {
            var photo = await uploader.UploadPhotoAndReturnFilePath(photos);

            model.Photo = photo;

            await admin.CreateBrand(model);

            return Redirect("/admin/products/create-new-product");
        }



        [HttpGet]
        [Route("/admin/produktai")]
        public IActionResult IndexSSS()
        {
            return View("Index");
        }

        // SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES SERIES

        [HttpPost]
        [Route("/admin/products/delete-series")]
        public async Task<IActionResult> DeleteSeries(int id)
        {
            await admin.DeleteSeries(id);

            return Redirect("/admin/products/series");
        }

        [HttpPost]
        [Route("/admin/products/update-series")]
        public async Task<IActionResult> UpdateSeries(UpdateSeriesViewModel s)
        {
            if (s.newPhoto != null)
            {
                var newPAth = await uploader.UploadPhotoAndReturnFilePath(s.newPhoto);
                s.Photo = newPAth;
            }

            var model = new SeriesModel()
            {
                Abbr = s.Abbr,
                Attributes = s.Attributes,
                BrandId = s.BrandId,
                CategoryId = s.CategoryId,
                Characteristics = s.Characteristics,
                CategoryName = s.CategoryName,
                Color = s.Color,
                Description = s.Description,
                idSeries = s.idSeries,
                Photo = s.Photo,
                SeriesName = s.SeriesName,
                ShortDescription = s.ShortDescription
            };


            await admin.UpdateSeries(model);

            return Redirect("/admin/products/series");
        }

        [HttpGet]
        [Route("/admin/products/series")]
        public async Task<IActionResult> Series()
        {
            var brands = await admin.GetAllSeries();

            return View("UpdateExistringSeries", new UpdateViewModelSeries() { Series = brands });
        }

        [HttpPost]
        [Route("/admin/products/series")]
        public async Task<IActionResult> Series(SeriesModel model)
        {
            var brands = await admin.GetAllSeries();

            return View("UpdateExistringSeries", new UpdateViewModelSeries() { Series = brands, CurrentSeries = model });
        }



        [HttpGet]
        [Route("/admin/discountCodes")]
        public async Task<IActionResult> Discount()
        {
            var discounts = await admin.GetAllDiscount();

            return View("IndexDiscount", discounts);
        }

        [HttpPost]
        [Route("/admin/discountCodes/create")]
        public async Task<IActionResult> CreateDiscount(DiscountModel model)
        {
            await admin.CreateDiscount(model);

            return Redirect("/admin/discountCodes");
        }

        [HttpPost]
        [Route("/admin/discountCodes/delete")]
        public async Task<IActionResult> DeleteDiscount(int idDiscount)
        {
            await admin.DeleteDiscount(idDiscount);

            return Redirect("/admin/discountCodes");
        }
    }
}

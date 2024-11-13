using BL.Auth;
using BL.Products;
using DAL.Models;
using EuroKatilai.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EuroKatilai.Controllers
{
    public class SearchProductController : Controller
    {
        private readonly IProducts product;

        public SearchProductController(IProducts product)
        {
            this.product = product;
        }

        [HttpGet]
        [Route("/product/quicksearch")]
        public async Task<IActionResult> Search(string searchQuery)
        {
            var results = await product.GetQuickSearchForSearch(searchQuery);
            return PartialView("_SearchResults", results);
        }

        [HttpGet]
        [Route("/product/search")]
        public async Task<IActionResult> Search(
            string searchFor,   
            FilterAZ f = FilterAZ.None,
            FilterPrice p = FilterPrice.None,
            int pageNumber = 1,
            int pageSize = 18)
        {
            if (string.IsNullOrEmpty(searchFor))
            {
                Redirect("/");
            }

            int totalProductCount = await product.CountProductsForSearch(searchFor);
            int totalPages = (int)Math.Ceiling(totalProductCount / (double)pageSize);

            var products = await product.GetProductsForSearch(searchFor, pageNumber, pageSize);

            switch (f)
            {
                case FilterAZ.None:
                    break;
                case FilterAZ.AZ:
                    products = products.OrderBy(x => x.FullName).ToList();
                    break;
                case FilterAZ.ZA:
                    products = products.OrderByDescending(x => x.FullName).ToList();
                    break;
                default:
                    f = FilterAZ.None;
                    break;
            }
            
            switch (p)
            {
                case FilterPrice.None:
                    break;
                case FilterPrice.Increasing:
                    products = products.OrderBy(x => x.Price).ToList();
                    break;
                case FilterPrice.Decreasing:
                    products = products.OrderByDescending(x => x.Price).ToList();
                    break;
                default:
                    p = FilterPrice.None;
                    break;
            }


            return View("Index", new SearchViewModel()
            {
                products = products.ToList(),
                searchFor = searchFor,
                f = f,
                p = p,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
            });
        }
    }

    public enum FilterAZ
    {
        AZ, ZA, None
    }

    public enum FilterPrice
    {
        Increasing, Decreasing, None
    }
}

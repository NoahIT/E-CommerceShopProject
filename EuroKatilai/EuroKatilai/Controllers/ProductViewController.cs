using BL.Auth;
using BL.Products;
using BL.Series;
using DAL.Models;
using EuroKatilai.ViewMapper;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Controllers
{
    public class ProductViewController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProducts products;
        private readonly ISeries series;

        public ProductViewController(ICurrentUser current, IProducts products, ISeries series)
        {
            this.currentUser = current;
            this.products = products;
            this.series = series;
        }

        [Route("products/{seriesId}")]
        [HttpGet]
        public async Task<IActionResult> IndexBySeries(int seriesId)
        {
            var tables = await products.GetProductTables(seriesId);
            var OneSeries = await series.GetSeriesBySeriesId(seriesId);


            if (OneSeries != null && OneSeries.Description != null && OneSeries.Characteristics != null) 
            {
                return View("Index",ProductViewMapper.MapperProductViewModel(tables,OneSeries));
            }


            return View("Error404");
        }
        

        [Route("product")]
        [HttpPost]
        public IActionResult IndexSave()
        {
            return View();
        }
    }
}

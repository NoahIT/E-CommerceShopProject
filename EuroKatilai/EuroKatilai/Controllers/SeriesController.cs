using BL.Auth;
using BL.Series;
using DAL.Models;
using EuroKatilai.ViewMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EuroKatilai.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly ISeries series;

        public SeriesController(ICurrentUser current, ISeries series)
        {
            this.currentUser = current;
            this.series = series;
        }

        [HttpGet]
        [Route("series/type/{abbrev}")]
        public async Task<IActionResult> IndexAbbrId(string abbrev)
        {
            var series = await this.series.GetSeriesByCategoryAsync(abbrev);

            if (isEmpty(series))
            {
                Response.StatusCode = 404;
                return View("Error404"); 
            }
            else
            {
                return View("Index", SeriesMapper.SeriesPageModelToSeriesViewModel(series));
            }
        }

        [HttpGet]
        [Route("series/brand/{id}")]
        public async Task<IActionResult> IndexBrandId(int id)
        {
            var series = await this.series.GetSeriesByBrandId(id);

            if (isEmpty(series))
            {
                Response.StatusCode = 404;
                return View("Error404"); 
            }
            else
            {
                return View("Index", SeriesMapper.SeriesPageModelToSeriesViewModel(series));
            }
        }

        [HttpPost]
        [Route("series")]
        public IActionResult IndexSave()
        {
            return View("Index");
        }

        private bool isEmpty(List<List<SeriesPageModel>> x)
        {
            if (x == null || x.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}

using DAL.Models;

namespace EuroKatilai.Areas.Admin.ViewModels
{
    public class UpdateSeriesViewModel
    {
        public int? idSeries { get; set; }
        public string SeriesName { get; set; }
        public int? BrandId { get; set; }
        public string? Photo { get; set; }
        public string ShortDescription { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Characteristics { get; set; }
        public string Color { get; set; }
        public string Attributes { get; set; }
        public string Abbr { get; set; }
        public IFormFile? newPhoto { get; set; }
    }
}

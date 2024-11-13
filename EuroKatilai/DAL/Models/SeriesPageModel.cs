namespace DAL.Models
{
    public class SeriesPageModel
    {
        public int? idSeries { get; set; }
        public string SeriesName { get; set; }
        public int? BrandId { get; set; }
        public string? Photo { get; set; }
        public string ShortDescription { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

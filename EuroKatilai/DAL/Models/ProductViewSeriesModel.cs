namespace DAL.Models
{
    public class ProductViewSeriesModel
    {
        public int idSeries { get; set; }
        public string SeriesName { get; set; }
        public int BrandId { get; set; }
        public string Photo { get; set; }
        public string? Description { get; set; }
        public string? Characteristics { get; set; }
        public Dictionary<string, string> DicCharacteristics { get; set; } = new Dictionary<string, string>();

    }
}

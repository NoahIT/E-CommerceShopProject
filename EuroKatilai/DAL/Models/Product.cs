namespace DAL.Models
{
    public class Product
    {
        public int? ProductId { get; set; }
        public int? SeriesID { get; set; }
        public string Model { get; set; }
        public double? Power { get; set; }
        public string Energy { get; set; }
        public string Attributes { get; set; }
        public Dictionary<string, string> DicAttributes { get; set; } = new Dictionary<string, string>();
        public decimal? Price { get; set; }
    }
}

namespace DAL.Models
{
    public class ProductTables
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public List<Product> Models { get; set; } = new List<Product>();
        public string ModelsId { get; set; }
        public List<int> ModelsIntList { get; set; } = new List<int>();
        public int SeriesId { get; set; }
    }
}

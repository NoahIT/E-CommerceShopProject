using DAL.Models;

namespace EuroKatilai.Areas.Admin.ViewModels
{
    public class UpdateViewModel
    {
        public List<Product> Products { get; set; }
        public Product CurrentProduct { get; set; } = new Product(){Attributes = "{\"Pavadinimas\": \"value\",\"Pavadinimas\": \"value\"}"};
    }
}

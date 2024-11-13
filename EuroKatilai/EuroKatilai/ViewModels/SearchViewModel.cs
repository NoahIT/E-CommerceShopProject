using DAL.Models;
using EuroKatilai.Controllers;

namespace EuroKatilai.ViewModels
{
    public class SearchViewModel
    {
        public List<ProductCardModel> products { get; set; }
        public string searchFor {get; set; }
        public FilterPrice p { get; set; }
        public FilterAZ f { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}

using DAL.Models;

namespace EuroKatilai.Areas.Admin.ViewModels
{
    public class UzsakymaiViewModel
    {
        public List<OrderModel> orders { get; set; }
        public string address { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OrderItemDetailsModel : OrderItemModel
    {
        public decimal Price { get; set; }

        public string Model { get; set; } = null!;
        public string SeriesName { get; set; } = null!;

        public string Photo { get; set; } = null!;
        public int idSeries { get; set; }
    }
}

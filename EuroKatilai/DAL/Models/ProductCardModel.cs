using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductCardModel
    {
        public int idSeries { get; set; }
        public int ProductId { get; set; }
        public string Photo { get; set; } = null!;

        public string ModelName { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string SeriesName { get; set; } = null!;
        public int Price { get; set; }


        public string FullName {
            get { return BrandName + " " + SeriesName + " " + ModelName; }
        }
    }
}

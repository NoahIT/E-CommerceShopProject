using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class QuickSearchModel
    {
        public int SeriesId { get; set; }

        public string Photo { get; set; }

        public string BrandName { get; set; }

        public string SeriesName { get; set; }

        public string ModelName { get; set; }

        public decimal Price { get; set; }
    }
}

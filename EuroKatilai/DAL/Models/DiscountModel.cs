using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DiscountModel
    {
        public int idDiscount { get; set; }
        public string Code { get; set; }
        public int Precent { get; set; }
    }
}

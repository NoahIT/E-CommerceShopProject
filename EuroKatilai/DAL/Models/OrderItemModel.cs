using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}

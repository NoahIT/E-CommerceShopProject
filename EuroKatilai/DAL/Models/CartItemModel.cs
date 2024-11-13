using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CartItemModel
    {
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public int ProductCount { get; set; }
    }
}

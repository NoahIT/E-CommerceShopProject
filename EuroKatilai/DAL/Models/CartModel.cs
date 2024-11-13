using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CartModel
    {
        public int? CartId { get; set; }

        public Guid? SessionId { get; set; }

        public int? UserId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
        public int? AddressId { get; set; }

        public int isArchived { get; set; }
        public int DiscountId { get; set; }
    }
}

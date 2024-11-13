using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OrderModel
    {
        public int? OrderId { get; set; }
        public Guid? SessionId { get; set; }
        public int? UserId { get; set; }
        public int? AddressId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Status { get; set; }
        public int Discount { get; set; }

    }
}

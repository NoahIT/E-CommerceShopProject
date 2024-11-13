﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class UserOrderModel
    {
        public decimal Total { get { return Items.Sum(m => m.Price * m.ProductCount); } }

        public int Count { get { return Items.Sum(m => m.ProductCount); } }
        public decimal TotalWithDiscount
        {
            get { return (Total - (Total / 100 * Discount)); }
        }

        public OrderModel Cart { get; set; } = null!;
        public int Discount { get; set; } = 0;
        public List<OrderItemDetailsModel> Items { get; set; } = new List<OrderItemDetailsModel>();
    }
}
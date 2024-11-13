using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductModelHome
    {
        public int idSeries { get; set; }
        public int ProductId { get; set; }
        public string Photo { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string SeriesName { get; set; }
    }

    //<div class="product">
    //    <img src = "~/Images/LaikinasKatilas.png" alt="Altera WZS 1">
    //    <h3 class="product-title">Alterra WZS</h3>
    //    <p class="product-price">€1000,00 EUR</p>
    //    <button class="section-button">Į krepšelį</button>
    //    </div>
}

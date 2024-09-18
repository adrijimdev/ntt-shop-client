using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Item
    {
        public int idProduct { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int units { get; set; }
        public decimal subtotal { get; set; }
    }
}
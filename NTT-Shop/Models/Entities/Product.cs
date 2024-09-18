using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace NTTShop.Models
{
    public class Product
    {
        public int idProduct { get; set; }
        public int stock { get; set; }
        public bool enabled { get; set; }
        public decimal price { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        //public List<ProductRate> rates = new List<ProductRate>();
        //public List<ProductDescription> descriptions = new List<ProductDescription>();
    }
}
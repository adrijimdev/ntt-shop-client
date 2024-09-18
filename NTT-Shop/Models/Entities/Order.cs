using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Order
    {
        public int idOrder { get; set; }
        public int idUser { get; set; }
        public DateTime orderDate { get; set; }
        public string orderStatus { get; set; }
        public decimal totalPrice { get; set; }
    }
}
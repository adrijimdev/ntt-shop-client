﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class User
    {
        public int pkUser { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname1 { get; set; }
        public string surname2 { get; set; }
        public string address { get; set; }
        public string province { get; set; }
        public string town { get; set; }
        public string postalcode { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string language { get; set; }
        public int rate { get; set; }
    }
}
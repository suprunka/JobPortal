using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebJobPortal.Models
{
    public class ShoppingCartView
    {
        public ShoppingCard Card{ get; set; }
        public string Error{ get; set; }
    }
}
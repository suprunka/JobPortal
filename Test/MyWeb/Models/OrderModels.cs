using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeb.Models
{
    public class OrderModel
    {
        public LinkedList<ServiceOfferViewModel> ListOfItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
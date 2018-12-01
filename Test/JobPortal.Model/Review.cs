using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
   public class OfferReview
    {
       public string CustomerId { get; set; }
       public string ServiceOfferId { get; set; }
       public string Comment { get; set; }
       public double Rate { get; set; }
    }

}

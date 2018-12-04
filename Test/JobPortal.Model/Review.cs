using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
   public class OfferReview
    {
       public virtual string CustomerId { get; set; }
       public virtual int ServiceOfferId { get; set; }
       public virtual string Comment { get; set; }
       public virtual double Rate { get; set; }
    }

}

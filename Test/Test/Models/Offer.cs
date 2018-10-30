using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public int RatePerHour { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

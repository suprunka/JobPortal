using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJobPortal.New
{
   public class ServiceAppModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public SubCategory Subcategory { get; set; }
        public decimal RatePerHour { get; set; }
        public string Author_phone { get; set; }
        public string FullName { get; set; }
        public virtual User Author { get; set; }

    }


}
using AppJobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SubcategoryOf : Attribute
    {
        public SubcategoryOf(Category cat)
        {
            Category = cat;
        }
        public Category Category { get; private set; }
    }
}

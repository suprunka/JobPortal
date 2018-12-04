using System;
using System.Linq;
using System.Reflection;

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
   
    public static class Extensions
    {
        public static bool IsSubcategoryOf(this SubCategory sub, Category cat)
        {
            Type t = typeof(SubCategory);
            System.Reflection.MemberInfo mi = t.GetMember(sub.ToString()).FirstOrDefault(m => m.GetCustomAttribute(typeof(SubcategoryOf)) != null);
            if (mi == null) throw new ArgumentException("Subcategory " + sub + " has no category.");
            SubcategoryOf subAttr = (SubcategoryOf)mi.GetCustomAttribute(typeof(SubcategoryOf));
            return subAttr.Category == cat;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebJobPortal.Validator
{
    public static class Validate
    {
        public static bool CheckValidation(object o)
        {
            var context = new ValidationContext(o);
            var results = new List<ValidationResult>();
            var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(o, context, results);
            if (isValid)
            {
                return true;
            }

            return false;
        }
    }
}




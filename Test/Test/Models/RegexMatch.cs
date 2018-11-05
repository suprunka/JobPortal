﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceLibrary.Models
{
    [Serializable]
    public static class RegexMatch
    {
        public static void DoesUserMatch(Users user)
        {

            if(Regex.IsMatch(user.UserName, "^[a-zA-Z0-9ÆæØøÅå]{4,}$") && 
                Regex.IsMatch(user.PhoneNumber, "^[0-9]{8}$")&&
                Regex.IsMatch(user.FirstName, "^[a-zA-Z0-9ÆæØøÅå]{1,}$") &&
                Regex.IsMatch(user.LastName, "^[a-zA-Z0-9ÆæØøÅå]{1,}$")&&
                Regex.IsMatch(user.Email, "^[a-zA-Z0-9ÆæØøÅå]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")&&
                Regex.IsMatch(user.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{4,}$") &&
                Regex.IsMatch(user.AddressLine,"^[a-zA-Z0-9ÆæØøÅå]{1,}$")&&
                Regex.IsMatch(user.CityName, "^[a-zA-Z0-9ÆæØøÅå]{1,}$")&&
                Regex.IsMatch(user.Postcode, "^[0-9]{4}$"))
            {

            }
            else
            {
                throw new ArgumentException();
            }
            
           
            
        }
    }
}
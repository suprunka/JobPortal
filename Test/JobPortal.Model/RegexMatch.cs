using AppJobPortal.Model;
using JobPortal.Model;
using System;
using System.Text.RegularExpressions;

namespace ServiceLibrary.Models
{

    public static class RegexMatch
    {
        public static bool DoesUserMatch(User user)
        {

            if(Regex.IsMatch(user.UserName, "^[a-zA-Z0-9ÆæØøÅå ]{4,}$") && 
                Regex.IsMatch(user.PhoneNumber, "^[0-9]{8}$")&&
                Regex.IsMatch(user.FirstName, "^[a-zA-Z0-9ÆæØøÅå ]{1,}$") &&
                Regex.IsMatch(user.LastName, "^[a-zA-Z0-9ÆæØøÅå ]{1,}$")&&
                Regex.IsMatch(user.Email, "^[a-zA-Z0-9ÆæØøÅå]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")&&
                Regex.IsMatch(user.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{4,}$") &&
                Regex.IsMatch(user.AddressLine,"^[a-zA-Z0-9ÆæØøÅå ]{1,}$")&&
                Regex.IsMatch(user.CityName, "^[a-zA-Z0-9ÆæØøÅå ]{1,}$")&&
                Regex.IsMatch(user.Postcode, "^[0-9]{4}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
            
           
            
        }

        public static bool DoesWebUserMatch(User user)
        {

            if (
                Regex.IsMatch(user.PhoneNumber, "^[0-9]{8}$") &&
                Regex.IsMatch(user.FirstName, "^[a-zA-Z0-9ÆæØøÅå ]{1,}$") &&
                Regex.IsMatch(user.LastName, "^[a-zA-Z0-9ÆæØøÅå ]{1,}$") &&
                Regex.IsMatch(user.AddressLine, "^[a-zA-Z0-9ÆæØøÅå ]{1,}$") &&
                Regex.IsMatch(user.CityName, "^[a-zA-Z0-9ÆæØøÅå ]{1,}$") &&
                Regex.IsMatch(user.Postcode, "^[0-9]{4}$"))
            {
                return true;
            }
            else
            {
                return false;
            }



        }
        public static bool DoesOfferMatch(Offer offer)
        {
            if (Regex.IsMatch(offer.Description, "^[a-zA-Z0-9ÆæØøÅå ]{10,}$") &&
                Regex.IsMatch(offer.Description, "^[a-zA-Z0-9ÆæØøÅå ]{5,}$")
                )
                //checking if the rate per hour isn't good to do in regex (as I read)
                return true;
            return false;
        }

    }
}

using JobPortal.Model;

using System.Text.RegularExpressions;

namespace ServiceLibrary.Models
{

    public static class RegexMatch
    {
        public static bool DoesUserMatch(User user)
        {

            if (Regex.IsMatch(user.FirstName, "^[a-zA-Z0-9ÆæØøÅå ]{1,}$") &&
                Regex.IsMatch(user.LastName, "^[a-zA-Z0-9ÆæØøÅå ]{1,}$") &&
                Regex.IsMatch(user.PayPalMail, "^[a-zA-Z0-9ÆæØøÅå]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$") &&
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

        public static bool DoesUserEmailMatch(User user)
        {

            if (Regex.IsMatch(user.Email, "^[a-zA-Z0-9ÆæØøÅå]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"))
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
            if (Regex.IsMatch(offer.Title, "^[a-zA-Z0-9ÆæØøÅå.,: ]{5,}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

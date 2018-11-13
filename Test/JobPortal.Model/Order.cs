using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
    public class Order
    {
        private User _user;
        private decimal _totalPrice;
        private LinkedList<Offer> _offers;
        private DateTime _date;

        public Order(User user)
        {
            _user = user;
            _date = DateTime.Now;
        }

        public LinkedList<Offer> AddOffer(Offer o, int numberOfHours)
        {
            if(o!= null)
            {
                _offers.AddLast(o);
                _totalPrice += (o.RatePerHour*numberOfHours);
            }
            return _offers;
        }

        






    }
}

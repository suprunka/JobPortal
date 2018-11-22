using JobPortal.Model;
using System.Collections.Generic;
using System.Linq;


namespace JobPortal.Model
{
    public class ShoppingCard
    {
        private User author;
        private LinkedList<OrderedOffer> listOfItems;
        

        public ShoppingCard(User u)
        {
            author = u;
            listOfItems = new LinkedList<OrderedOffer>();

        }

        public User Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
            }
        }

        public LinkedList<OrderedOffer> List
        {
            get
            {
                return listOfItems;
            }
            private set
            {
                listOfItems = value;
            }
        }

        public LinkedList<OrderedOffer> AddToCard(OrderedOffer orderedOffer)
        {
            if (orderedOffer != null)
            {
                listOfItems.AddLast(orderedOffer);
                return listOfItems;
            }
            return null;
        }

        public LinkedList<OrderedOffer> RemoveFromCard(int id)
        {

            var found = listOfItems.Single(x => x.Id == id);
            if(found == null)
            {
                return null;
            }
            else
            {
                listOfItems.Remove(found);
                return listOfItems;
            }
        }

        public LinkedList<OrderedOffer> EmptyCard()
        {
            listOfItems.Clear();
            return listOfItems;
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            foreach(var i in listOfItems)
            {
                totalPrice += i.RatePerHour * (i.HoursTo - i.HoursFrom).Hours;
            }
            return totalPrice;
        }
    }
}
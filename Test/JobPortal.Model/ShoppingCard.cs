using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace JobPortal.Model
{
    [Serializable]
    public class ShoppingCard
    {
        private User author;
        private LinkedList<OrderedOffer> listOfItems;
        private LinkedList<PayPalOffer> payPallistOfItems;

        public ShoppingCard(User u)
        {
            author = u;
            listOfItems = new LinkedList<OrderedOffer>();
            payPallistOfItems = new LinkedList<PayPalOffer>();
        }

        public ShoppingCard()
        {
            listOfItems = new LinkedList<OrderedOffer>();
            payPallistOfItems = new LinkedList<PayPalOffer>();
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

        public LinkedList<PayPalOffer> PayPalList
        {
            get
            {
                return payPallistOfItems;
            }
            private set
            {
                payPallistOfItems = value;
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

        public LinkedList<PayPalOffer> AddToCardPayPal(PayPalOffer orderedOffer)
        {
            if (orderedOffer != null)
            {
                payPallistOfItems.AddLast(orderedOffer);
                return payPallistOfItems;
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

        public decimal GetTotalPricePayPal()
        {
            decimal totalPrice = 0;
            foreach (var i in payPallistOfItems)
            {
                totalPrice += i.RatePerHour * (i.HoursTo - i.HoursFrom).Hours;
            }
            return totalPrice;
        }
    }
}
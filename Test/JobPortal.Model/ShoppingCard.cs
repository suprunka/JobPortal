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
        private LinkedList<Offer> listOfItems;
        private LinkedList<PayPalOffer> payPallistOfItems;
        private Random random;
        public ShoppingCard(User u)
        {
            author = u;
            listOfItems = new LinkedList<Offer>();
            payPallistOfItems = new LinkedList<PayPalOffer>();
            random = new Random();
        }

        public ShoppingCard()
        {
            listOfItems = new LinkedList<Offer>();
            payPallistOfItems = new LinkedList<PayPalOffer>();
            random = new Random();
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

        public LinkedList<Offer> List
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

        public LinkedList<Offer> AddToCard(Offer orderedOffer)
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


        public LinkedList<Offer> RemoveFromCard(int id)
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

        public LinkedList<Offer> EmptyCard()
        {
            listOfItems.Clear();
            return listOfItems;
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            foreach(var i in listOfItems)
            {
                totalPrice += i.RatePerHour * (i.WorkingTime.HoursTo - i.WorkingTime.HoursFrom).Hours;
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


        public string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 15)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
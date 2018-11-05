using JobPortal.Model;
using System;
using System.Text.RegularExpressions;


namespace ServiceLibrary.Models
{

    public class Offer
    {
        public int Id { get; set; }

        public int RatePerHour { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}

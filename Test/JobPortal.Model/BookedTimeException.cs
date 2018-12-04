using System;

namespace JobPortal.Model
{
    [Serializable]
    public class BookedTimeException : Exception
    {
        public int OfferId { get; set; }
        public string EmployeeId { get; set; }
        public TimeSpan HourFrom { get; set; }
        public TimeSpan HourTo { get; set; }
        public DateTime DateOfOffer { get; set; }
        public string Title { get; set; }

        public BookedTimeException(int offerId, string employee_ID, TimeSpan from, TimeSpan to, DateTime date, string title)
        {
            this.OfferId = offerId;
            this.EmployeeId = employee_ID;
            this.HourFrom = from;
            this.HourTo = to;
            this.DateOfOffer = date;
            this.Title = title;
        }

        public String GetMessage()
        {
            return "The offer " + Title + " is not available from: " + HourFrom.ToString() + ", to: " + HourTo.ToString() + " on: " + DateOfOffer.ToShortDateString();

        }

    }
}

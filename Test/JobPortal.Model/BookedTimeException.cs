using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
    [Serializable]
    public class BookedTimeException : Exception
    {
        public int OfferId { get; set; }
        public string EmployeeId { get; set; }

        public BookedTimeException(int offerId, string employee_ID)
        {
            this.OfferId = offerId;
            this.EmployeeId = employee_ID;
        }
    }
}

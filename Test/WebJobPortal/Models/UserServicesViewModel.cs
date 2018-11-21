using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebJobPortal.Models
{
    public class UserServicesViewModel
    {
        public UserModel User { get; set; }
        public IEnumerable<ServiceOfferWebModel> Services { get; set; }
    }
}
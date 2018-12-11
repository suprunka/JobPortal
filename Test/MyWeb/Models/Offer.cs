using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Models
{
    [Serializable]
    [DataContract]
    public class Offer
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int RatePerHour { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}

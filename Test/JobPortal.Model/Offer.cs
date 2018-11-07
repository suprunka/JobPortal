using JobPortal.Model;
using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;


namespace AppJobPortal.Model
{
    [DataContract]
    public class Offer
    {
    [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual int RatePerHour { get; set; }
        [DataMember]
        public virtual string Title { get; set; }
        [DataMember]
        public virtual string Description { get; set; }
        [DataMember]
        public virtual User Author { get; set; }
        [DataMember]
        public virtual string CategoryName { get; set; }
        [DataMember]
        public virtual string CategoryDescription { get; set; }
        [DataMember]
        public virtual string ServiceName { get; set; }

    }
}

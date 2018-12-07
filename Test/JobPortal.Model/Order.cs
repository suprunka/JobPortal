using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public virtual int ID { get; set; }
        [DataMember]
        public virtual string User_ID { get; set; }
        [DataMember]
        public virtual decimal TotalPrice { get; set; }
        [DataMember]
        public virtual string OrderStatus { get; set; }
        [DataMember]
        public virtual IEnumerable<Saleline> Salelines { get; set; }


    }

    [DataContract]
    public class JobOffer
    {
        [DataMember]
        public virtual string Customer_ID { get; set; }
        [DataMember]
        public virtual User Customer { get; set; }
        [DataMember]
        public virtual decimal TotalPrice { get; set; }
        [DataMember]
        public virtual Offer Offer { get; set; }


    }

    [DataContract]
    public class Saleline
    {
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual int ServiceOfferId { get; set; }
        [DataMember]
        public virtual string AuthorId { get; set; }
        [DataMember]
        public virtual DateTime Date { get; set; }
    }


}

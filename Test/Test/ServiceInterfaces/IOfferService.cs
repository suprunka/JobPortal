using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Description;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IOfferService
    {

        [OperationContract]
        Offer CreateServiceOffer(Offer serviceOffer);

        [OperationContract]
        Offer FindServiceOffer(int ID);

        [OperationContract]
        bool DeleteServiceOffer(int ID);

        [OperationContract]
        bool UpdateServiceOffer(Offer serviceOffer);

        [OperationContract]
        IQueryable<Offer> GetAllOffers();

    }
}

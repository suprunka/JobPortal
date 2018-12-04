﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyWeb.OfferReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OfferReference.IOfferService")]
    public interface IOfferService {
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/CreateServiceOffer", ReplyAction="http://tempuri.org/IOfferService/CreateServiceOfferResponse")]
        bool CreateServiceOffer(JobPortal.Model.Offer offer);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/CreateServiceOffer", ReplyAction="http://tempuri.org/IOfferService/CreateServiceOfferResponse")]
        System.Threading.Tasks.Task<bool> CreateServiceOfferAsync(JobPortal.Model.Offer offer);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/FindServiceOffer", ReplyAction="http://tempuri.org/IOfferService/FindServiceOfferResponse")]
        JobPortal.Model.Offer FindServiceOffer(int ID);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/FindServiceOffer", ReplyAction="http://tempuri.org/IOfferService/FindServiceOfferResponse")]
        System.Threading.Tasks.Task<JobPortal.Model.Offer> FindServiceOfferAsync(int ID);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/DeleteServiceOffer", ReplyAction="http://tempuri.org/IOfferService/DeleteServiceOfferResponse")]
        bool DeleteServiceOffer(int ID);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/DeleteServiceOffer", ReplyAction="http://tempuri.org/IOfferService/DeleteServiceOfferResponse")]
        System.Threading.Tasks.Task<bool> DeleteServiceOfferAsync(int ID);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/UpdateServiceOffer", ReplyAction="http://tempuri.org/IOfferService/UpdateServiceOfferResponse")]
        bool UpdateServiceOffer(JobPortal.Model.Offer serviceOffer);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/UpdateServiceOffer", ReplyAction="http://tempuri.org/IOfferService/UpdateServiceOfferResponse")]
        System.Threading.Tasks.Task<bool> UpdateServiceOfferAsync(JobPortal.Model.Offer serviceOffer);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/GetAllOffers", ReplyAction="http://tempuri.org/IOfferService/GetAllOffersResponse")]
        JobPortal.Model.Offer[] GetAllOffers();
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/GetAllOffers", ReplyAction="http://tempuri.org/IOfferService/GetAllOffersResponse")]
        System.Threading.Tasks.Task<JobPortal.Model.Offer[]> GetAllOffersAsync();
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/AddHoursToOffer", ReplyAction="http://tempuri.org/IOfferService/AddHoursToOfferResponse")]
        bool AddHoursToOffer(JobPortal.Model.WorkingTime time);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/AddHoursToOffer", ReplyAction="http://tempuri.org/IOfferService/AddHoursToOfferResponse")]
        System.Threading.Tasks.Task<bool> AddHoursToOfferAsync(JobPortal.Model.WorkingTime time);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/GetAllWorkingDays", ReplyAction="http://tempuri.org/IOfferService/GetAllWorkingDaysResponse")]
        JobPortal.Model.WorkingTime[] GetAllWorkingDays();
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/GetAllWorkingDays", ReplyAction="http://tempuri.org/IOfferService/GetAllWorkingDaysResponse")]
        System.Threading.Tasks.Task<JobPortal.Model.WorkingTime[]> GetAllWorkingDaysAsync();
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/GetAllBought", ReplyAction="http://tempuri.org/IOfferService/GetAllBoughtResponse")]
        JobPortal.Model.Offer[] GetAllBought(string ID);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/GetAllBought", ReplyAction="http://tempuri.org/IOfferService/GetAllBoughtResponse")]
        System.Threading.Tasks.Task<JobPortal.Model.Offer[]> GetAllBoughtAsync(string ID);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/AddReview", ReplyAction="http://tempuri.org/IOfferService/AddReviewResponse")]
        bool AddReview(JobPortal.Model.OfferReview review);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/AddReview", ReplyAction="http://tempuri.org/IOfferService/AddReviewResponse")]
        System.Threading.Tasks.Task<bool> AddReviewAsync(JobPortal.Model.OfferReview review);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/GetServiceReviews", ReplyAction="http://tempuri.org/IOfferService/GetServiceReviewsResponse")]
        JobPortal.Model.OfferReview[] GetServiceReviews(int serviceId);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/GetServiceReviews", ReplyAction="http://tempuri.org/IOfferService/GetServiceReviewsResponse")]
        System.Threading.Tasks.Task<JobPortal.Model.OfferReview[]> GetServiceReviewsAsync(int serviceId);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/GetAvgOfServiceRates", ReplyAction="http://tempuri.org/IOfferService/GetAvgOfServiceRatesResponse")]
        int GetAvgOfServiceRates(int serviceId);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/IOfferService/GetAvgOfServiceRates", ReplyAction="http://tempuri.org/IOfferService/GetAvgOfServiceRatesResponse")]
        System.Threading.Tasks.Task<int> GetAvgOfServiceRatesAsync(int serviceId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOfferServiceChannel : MyWeb.OfferReference.IOfferService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OfferServiceClient : System.ServiceModel.ClientBase<MyWeb.OfferReference.IOfferService>, MyWeb.OfferReference.IOfferService {
        
        public OfferServiceClient() {
        }
        
        public OfferServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OfferServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OfferServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OfferServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool CreateServiceOffer(JobPortal.Model.Offer offer) {
            return base.Channel.CreateServiceOffer(offer);
        }
        
        public System.Threading.Tasks.Task<bool> CreateServiceOfferAsync(JobPortal.Model.Offer offer) {
            return base.Channel.CreateServiceOfferAsync(offer);
        }
        
        public JobPortal.Model.Offer FindServiceOffer(int ID) {
            return base.Channel.FindServiceOffer(ID);
        }
        
        public System.Threading.Tasks.Task<JobPortal.Model.Offer> FindServiceOfferAsync(int ID) {
            return base.Channel.FindServiceOfferAsync(ID);
        }
        
        public bool DeleteServiceOffer(int ID) {
            return base.Channel.DeleteServiceOffer(ID);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteServiceOfferAsync(int ID) {
            return base.Channel.DeleteServiceOfferAsync(ID);
        }
        
        public bool UpdateServiceOffer(JobPortal.Model.Offer serviceOffer) {
            return base.Channel.UpdateServiceOffer(serviceOffer);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateServiceOfferAsync(JobPortal.Model.Offer serviceOffer) {
            return base.Channel.UpdateServiceOfferAsync(serviceOffer);
        }
        
        public JobPortal.Model.Offer[] GetAllOffers() {
            return base.Channel.GetAllOffers();
        }
        
        public System.Threading.Tasks.Task<JobPortal.Model.Offer[]> GetAllOffersAsync() {
            return base.Channel.GetAllOffersAsync();
        }
        
        public bool AddHoursToOffer(JobPortal.Model.WorkingTime time) {
            return base.Channel.AddHoursToOffer(time);
        }
        
        public System.Threading.Tasks.Task<bool> AddHoursToOfferAsync(JobPortal.Model.WorkingTime time) {
            return base.Channel.AddHoursToOfferAsync(time);
        }
        
        public JobPortal.Model.WorkingTime[] GetAllWorkingDays() {
            return base.Channel.GetAllWorkingDays();
        }
        
        public System.Threading.Tasks.Task<JobPortal.Model.WorkingTime[]> GetAllWorkingDaysAsync() {
            return base.Channel.GetAllWorkingDaysAsync();
        }
        
        public JobPortal.Model.Offer[] GetAllBought(string ID) {
            return base.Channel.GetAllBought(ID);
        }
        
        public System.Threading.Tasks.Task<JobPortal.Model.Offer[]> GetAllBoughtAsync(string ID) {
            return base.Channel.GetAllBoughtAsync(ID);
        }
        
        public bool AddReview(JobPortal.Model.OfferReview review) {
            return base.Channel.AddReview(review);
        }
        
        public System.Threading.Tasks.Task<bool> AddReviewAsync(JobPortal.Model.OfferReview review) {
            return base.Channel.AddReviewAsync(review);
        }
        
        public JobPortal.Model.OfferReview[] GetServiceReviews(int serviceId) {
            return base.Channel.GetServiceReviews(serviceId);
        }
        
        public System.Threading.Tasks.Task<JobPortal.Model.OfferReview[]> GetServiceReviewsAsync(int serviceId) {
            return base.Channel.GetServiceReviewsAsync(serviceId);
        }
        
        public int GetAvgOfServiceRates(int serviceId) {
            return base.Channel.GetAvgOfServiceRates(serviceId);
        }
        
        public System.Threading.Tasks.Task<int> GetAvgOfServiceRatesAsync(int serviceId) {
            return base.Channel.GetAvgOfServiceRatesAsync(serviceId);
        }
    }
}

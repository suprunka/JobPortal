﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyWeb.OrderReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OrderReference.IOrderService")]
    public interface IOrderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CreateOrder", ReplyAction="http://tempuri.org/IOrderService/CreateOrderResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(JobPortal.Model.BookedTimeException), Action="http://tempuri.org/IOrderService/CreateOrderBookedTimeExceptionFault", Name="BookedTimeException", Namespace="http://schemas.datacontract.org/2004/07/JobPortal.Model")]
        JobPortal.Model.Order CreateOrder(string Logging_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CreateOrder", ReplyAction="http://tempuri.org/IOrderService/CreateOrderResponse")]
        System.Threading.Tasks.Task<JobPortal.Model.Order> CreateOrderAsync(string Logging_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CancelOrder", ReplyAction="http://tempuri.org/IOrderService/CancelOrderResponse")]
        bool CancelOrder(JobPortal.Model.Order o);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CancelOrder", ReplyAction="http://tempuri.org/IOrderService/CancelOrderResponse")]
        System.Threading.Tasks.Task<bool> CancelOrderAsync(JobPortal.Model.Order o);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetShoppingCart", ReplyAction="http://tempuri.org/IOrderService/GetShoppingCartResponse")]
        JobPortal.Model.ShoppingCard GetShoppingCart(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetShoppingCart", ReplyAction="http://tempuri.org/IOrderService/GetShoppingCartResponse")]
        System.Threading.Tasks.Task<JobPortal.Model.ShoppingCard> GetShoppingCartAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/PayForOrder", ReplyAction="http://tempuri.org/IOrderService/PayForOrderResponse")]
        bool PayForOrder(JobPortal.Model.Order o);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/PayForOrder", ReplyAction="http://tempuri.org/IOrderService/PayForOrderResponse")]
        System.Threading.Tasks.Task<bool> PayForOrderAsync(JobPortal.Model.Order o);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CleanCart", ReplyAction="http://tempuri.org/IOrderService/CleanCartResponse")]
        bool CleanCart(string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CleanCart", ReplyAction="http://tempuri.org/IOrderService/CleanCartResponse")]
        System.Threading.Tasks.Task<bool> CleanCartAsync(string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/AddToCart", ReplyAction="http://tempuri.org/IOrderService/AddToCartResponse")]
        bool AddToCart(string userId, int serviceId, System.DateTime date, System.TimeSpan hourfrom, System.TimeSpan hourTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/AddToCart", ReplyAction="http://tempuri.org/IOrderService/AddToCartResponse")]
        System.Threading.Tasks.Task<bool> AddToCartAsync(string userId, int serviceId, System.DateTime date, System.TimeSpan hourfrom, System.TimeSpan hourTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/DeleteFromCart", ReplyAction="http://tempuri.org/IOrderService/DeleteFromCartResponse")]
        bool DeleteFromCart(string userId, int serviceId, System.DateTime date, System.TimeSpan hourfrom, System.TimeSpan hourTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/DeleteFromCart", ReplyAction="http://tempuri.org/IOrderService/DeleteFromCartResponse")]
        System.Threading.Tasks.Task<bool> DeleteFromCartAsync(string userId, int serviceId, System.DateTime date, System.TimeSpan hourfrom, System.TimeSpan hourTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetHoursFrom", ReplyAction="http://tempuri.org/IOrderService/GetHoursFromResponse")]
        System.TimeSpan[] GetHoursFrom(int serviceId, System.DateTime date);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetHoursFrom", ReplyAction="http://tempuri.org/IOrderService/GetHoursFromResponse")]
        System.Threading.Tasks.Task<System.TimeSpan[]> GetHoursFromAsync(int serviceId, System.DateTime date);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetHoursTo", ReplyAction="http://tempuri.org/IOrderService/GetHoursToResponse")]
        System.TimeSpan[] GetHoursTo(int serviceId, System.DateTime date, System.TimeSpan from);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetHoursTo", ReplyAction="http://tempuri.org/IOrderService/GetHoursToResponse")]
        System.Threading.Tasks.Task<System.TimeSpan[]> GetHoursToAsync(int serviceId, System.DateTime date, System.TimeSpan from);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetShoppingCartForPaypal", ReplyAction="http://tempuri.org/IOrderService/GetShoppingCartForPaypalResponse")]
        JobPortal.Model.ShoppingCard GetShoppingCartForPaypal(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetShoppingCartForPaypal", ReplyAction="http://tempuri.org/IOrderService/GetShoppingCartForPaypalResponse")]
        System.Threading.Tasks.Task<JobPortal.Model.ShoppingCard> GetShoppingCartForPaypalAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetJobCallendar", ReplyAction="http://tempuri.org/IOrderService/GetJobCallendarResponse")]
        JobPortal.Model.JobOffer[] GetJobCallendar(System.DateTime date, string employeeId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetJobCallendar", ReplyAction="http://tempuri.org/IOrderService/GetJobCallendarResponse")]
        System.Threading.Tasks.Task<JobPortal.Model.JobOffer[]> GetJobCallendarAsync(System.DateTime date, string employeeId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/FindOrder", ReplyAction="http://tempuri.org/IOrderService/FindOrderResponse")]
        JobPortal.Model.Order FindOrder(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/FindOrder", ReplyAction="http://tempuri.org/IOrderService/FindOrderResponse")]
        System.Threading.Tasks.Task<JobPortal.Model.Order> FindOrderAsync(string id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderServiceChannel : MyWeb.OrderReference.IOrderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderServiceClient : System.ServiceModel.ClientBase<MyWeb.OrderReference.IOrderService>, MyWeb.OrderReference.IOrderService {
        
        public OrderServiceClient() {
        }
        
        public OrderServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public JobPortal.Model.Order CreateOrder(string Logging_ID) {
            return base.Channel.CreateOrder(Logging_ID);
        }
        
        public System.Threading.Tasks.Task<JobPortal.Model.Order> CreateOrderAsync(string Logging_ID) {
            return base.Channel.CreateOrderAsync(Logging_ID);
        }
        
        public bool CancelOrder(JobPortal.Model.Order o) {
            return base.Channel.CancelOrder(o);
        }
        
        public System.Threading.Tasks.Task<bool> CancelOrderAsync(JobPortal.Model.Order o) {
            return base.Channel.CancelOrderAsync(o);
        }
        
        public JobPortal.Model.ShoppingCard GetShoppingCart(string id) {
            return base.Channel.GetShoppingCart(id);
        }
        
        public System.Threading.Tasks.Task<JobPortal.Model.ShoppingCard> GetShoppingCartAsync(string id) {
            return base.Channel.GetShoppingCartAsync(id);
        }
        
        public bool PayForOrder(JobPortal.Model.Order o) {
            return base.Channel.PayForOrder(o);
        }
        
        public System.Threading.Tasks.Task<bool> PayForOrderAsync(JobPortal.Model.Order o) {
            return base.Channel.PayForOrderAsync(o);
        }
        
        public bool CleanCart(string userId) {
            return base.Channel.CleanCart(userId);
        }
        
        public System.Threading.Tasks.Task<bool> CleanCartAsync(string userId) {
            return base.Channel.CleanCartAsync(userId);
        }
        
        public bool AddToCart(string userId, int serviceId, System.DateTime date, System.TimeSpan hourfrom, System.TimeSpan hourTo) {
            return base.Channel.AddToCart(userId, serviceId, date, hourfrom, hourTo);
        }
        
        public System.Threading.Tasks.Task<bool> AddToCartAsync(string userId, int serviceId, System.DateTime date, System.TimeSpan hourfrom, System.TimeSpan hourTo) {
            return base.Channel.AddToCartAsync(userId, serviceId, date, hourfrom, hourTo);
        }
        
        public bool DeleteFromCart(string userId, int serviceId, System.DateTime date, System.TimeSpan hourfrom, System.TimeSpan hourTo) {
            return base.Channel.DeleteFromCart(userId, serviceId, date, hourfrom, hourTo);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteFromCartAsync(string userId, int serviceId, System.DateTime date, System.TimeSpan hourfrom, System.TimeSpan hourTo) {
            return base.Channel.DeleteFromCartAsync(userId, serviceId, date, hourfrom, hourTo);
        }
        
        public System.TimeSpan[] GetHoursFrom(int serviceId, System.DateTime date) {
            return base.Channel.GetHoursFrom(serviceId, date);
        }
        
        public System.Threading.Tasks.Task<System.TimeSpan[]> GetHoursFromAsync(int serviceId, System.DateTime date) {
            return base.Channel.GetHoursFromAsync(serviceId, date);
        }
        
        public System.TimeSpan[] GetHoursTo(int serviceId, System.DateTime date, System.TimeSpan from) {
            return base.Channel.GetHoursTo(serviceId, date, from);
        }
        
        public System.Threading.Tasks.Task<System.TimeSpan[]> GetHoursToAsync(int serviceId, System.DateTime date, System.TimeSpan from) {
            return base.Channel.GetHoursToAsync(serviceId, date, from);
        }
        
        public JobPortal.Model.ShoppingCard GetShoppingCartForPaypal(string id) {
            return base.Channel.GetShoppingCartForPaypal(id);
        }
        
        public System.Threading.Tasks.Task<JobPortal.Model.ShoppingCard> GetShoppingCartForPaypalAsync(string id) {
            return base.Channel.GetShoppingCartForPaypalAsync(id);
        }
        
        public JobPortal.Model.JobOffer[] GetJobCallendar(System.DateTime date, string employeeId) {
            return base.Channel.GetJobCallendar(date, employeeId);
        }
        
        public System.Threading.Tasks.Task<JobPortal.Model.JobOffer[]> GetJobCallendarAsync(System.DateTime date, string employeeId) {
            return base.Channel.GetJobCallendarAsync(date, employeeId);
        }
        
        public JobPortal.Model.Order FindOrder(string id) {
            return base.Channel.FindOrder(id);
        }
        
        public System.Threading.Tasks.Task<JobPortal.Model.Order> FindOrderAsync(string id) {
            return base.Channel.FindOrderAsync(id);
        }
    }
}

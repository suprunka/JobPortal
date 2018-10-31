﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebJobPortal.UserServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserServiceReference.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/CreateUser", ReplyAction="http://tempuri.org/IUserService/CreateUserResponse")]
        ServiceLibrary.Models.User CreateUser(ServiceLibrary.Models.User u);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/CreateUser", ReplyAction="http://tempuri.org/IUserService/CreateUserResponse")]
        System.Threading.Tasks.Task<ServiceLibrary.Models.User> CreateUserAsync(ServiceLibrary.Models.User u);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/FindUser", ReplyAction="http://tempuri.org/IUserService/FindUserResponse")]
        ServiceLibrary.Models.User FindUser(string PhoneNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/FindUser", ReplyAction="http://tempuri.org/IUserService/FindUserResponse")]
        System.Threading.Tasks.Task<ServiceLibrary.Models.User> FindUserAsync(string PhoneNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUser", ReplyAction="http://tempuri.org/IUserService/DeleteUserResponse")]
        bool DeleteUser(string PhoneNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUser", ReplyAction="http://tempuri.org/IUserService/DeleteUserResponse")]
        System.Threading.Tasks.Task<bool> DeleteUserAsync(string PhoneNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/EditUser", ReplyAction="http://tempuri.org/IUserService/EditUserResponse")]
        bool EditUser(ServiceLibrary.Models.User u);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/EditUser", ReplyAction="http://tempuri.org/IUserService/EditUserResponse")]
        System.Threading.Tasks.Task<bool> EditUserAsync(ServiceLibrary.Models.User u);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : WebJobPortal.UserServiceReference.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<WebJobPortal.UserServiceReference.IUserService>, WebJobPortal.UserServiceReference.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ServiceLibrary.Models.User CreateUser(ServiceLibrary.Models.User u) {
            return base.Channel.CreateUser(u);
        }
        
        public System.Threading.Tasks.Task<ServiceLibrary.Models.User> CreateUserAsync(ServiceLibrary.Models.User u) {
            return base.Channel.CreateUserAsync(u);
        }
        
        public ServiceLibrary.Models.User FindUser(string PhoneNumber) {
            return base.Channel.FindUser(PhoneNumber);
        }
        
        public System.Threading.Tasks.Task<ServiceLibrary.Models.User> FindUserAsync(string PhoneNumber) {
            return base.Channel.FindUserAsync(PhoneNumber);
        }
        
        public bool DeleteUser(string PhoneNumber) {
            return base.Channel.DeleteUser(PhoneNumber);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteUserAsync(string PhoneNumber) {
            return base.Channel.DeleteUserAsync(PhoneNumber);
        }
        
        public bool EditUser(ServiceLibrary.Models.User u) {
            return base.Channel.EditUser(u);
        }
        
        public System.Threading.Tasks.Task<bool> EditUserAsync(ServiceLibrary.Models.User u) {
            return base.Channel.EditUserAsync(u);
        }
    }
}

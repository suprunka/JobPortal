﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppJobPortal.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/ServiceLibrary.Models")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressLineField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CityNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AppJobPortal.ServiceReference.Gender GenderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PostcodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AppJobPortal.ServiceReference.Region RegionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AddressLine {
            get {
                return this.AddressLineField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressLineField, value) != true)) {
                    this.AddressLineField = value;
                    this.RaisePropertyChanged("AddressLine");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CityName {
            get {
                return this.CityNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CityNameField, value) != true)) {
                    this.CityNameField = value;
                    this.RaisePropertyChanged("CityName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AppJobPortal.ServiceReference.Gender Gender {
            get {
                return this.GenderField;
            }
            set {
                if ((this.GenderField.Equals(value) != true)) {
                    this.GenderField = value;
                    this.RaisePropertyChanged("Gender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhoneNumber {
            get {
                return this.PhoneNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneNumberField, value) != true)) {
                    this.PhoneNumberField = value;
                    this.RaisePropertyChanged("PhoneNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Postcode {
            get {
                return this.PostcodeField;
            }
            set {
                if ((object.ReferenceEquals(this.PostcodeField, value) != true)) {
                    this.PostcodeField = value;
                    this.RaisePropertyChanged("Postcode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AppJobPortal.ServiceReference.Region Region {
            get {
                return this.RegionField;
            }
            set {
                if ((this.RegionField.Equals(value) != true)) {
                    this.RegionField = value;
                    this.RaisePropertyChanged("Region");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Gender", Namespace="http://schemas.datacontract.org/2004/07/ServiceLibrary.Models")]
    public enum Gender : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Male = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Female = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Region", Namespace="http://schemas.datacontract.org/2004/07/ServiceLibrary.Models")]
    public enum Region : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Hovedstaden = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Midtjylland = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Nordjylland = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sjælland = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Syddanmark = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/CreateUser", ReplyAction="http://tempuri.org/IUserService/CreateUserResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(AppJobPortal.ServiceReference.User), Action="http://tempuri.org/IUserService/CreateUserUserFault", Name="User", Namespace="http://schemas.datacontract.org/2004/07/ServiceLibrary.Models")]
        AppJobPortal.ServiceReference.User CreateUser(AppJobPortal.ServiceReference.User u);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/CreateUser", ReplyAction="http://tempuri.org/IUserService/CreateUserResponse")]
        System.Threading.Tasks.Task<AppJobPortal.ServiceReference.User> CreateUserAsync(AppJobPortal.ServiceReference.User u);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/FindUser", ReplyAction="http://tempuri.org/IUserService/FindUserResponse")]
        AppJobPortal.ServiceReference.User FindUser(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/FindUser", ReplyAction="http://tempuri.org/IUserService/FindUserResponse")]
        System.Threading.Tasks.Task<AppJobPortal.ServiceReference.User> FindUserAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUser", ReplyAction="http://tempuri.org/IUserService/DeleteUserResponse")]
        bool DeleteUser(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUser", ReplyAction="http://tempuri.org/IUserService/DeleteUserResponse")]
        System.Threading.Tasks.Task<bool> DeleteUserAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/EditUser", ReplyAction="http://tempuri.org/IUserService/EditUserResponse")]
        bool EditUser(AppJobPortal.ServiceReference.User u);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/EditUser", ReplyAction="http://tempuri.org/IUserService/EditUserResponse")]
        System.Threading.Tasks.Task<bool> EditUserAsync(AppJobPortal.ServiceReference.User u);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAll", ReplyAction="http://tempuri.org/IUserService/GetAllResponse")]
        AppJobPortal.ServiceReference.User[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAll", ReplyAction="http://tempuri.org/IUserService/GetAllResponse")]
        System.Threading.Tasks.Task<AppJobPortal.ServiceReference.User[]> GetAllAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : AppJobPortal.ServiceReference.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<AppJobPortal.ServiceReference.IUserService>, AppJobPortal.ServiceReference.IUserService {
        
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
        
        public AppJobPortal.ServiceReference.User CreateUser(AppJobPortal.ServiceReference.User u) {
            return base.Channel.CreateUser(u);
        }
        
        public System.Threading.Tasks.Task<AppJobPortal.ServiceReference.User> CreateUserAsync(AppJobPortal.ServiceReference.User u) {
            return base.Channel.CreateUserAsync(u);
        }
        
        public AppJobPortal.ServiceReference.User FindUser(int id) {
            return base.Channel.FindUser(id);
        }
        
        public System.Threading.Tasks.Task<AppJobPortal.ServiceReference.User> FindUserAsync(int id) {
            return base.Channel.FindUserAsync(id);
        }
        
        public bool DeleteUser(int id) {
            return base.Channel.DeleteUser(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteUserAsync(int id) {
            return base.Channel.DeleteUserAsync(id);
        }
        
        public bool EditUser(AppJobPortal.ServiceReference.User u) {
            return base.Channel.EditUser(u);
        }
        
        public System.Threading.Tasks.Task<bool> EditUserAsync(AppJobPortal.ServiceReference.User u) {
            return base.Channel.EditUserAsync(u);
        }
        
        public AppJobPortal.ServiceReference.User[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<AppJobPortal.ServiceReference.User[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
    }
}

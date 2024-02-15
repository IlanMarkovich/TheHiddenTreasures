﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 17.0.34205.153
// 
namespace TheHiddenTreasures.ServiceReference1 {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/TheHiddenTreasuresWCF")]
    public partial class User : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string passwordField;
        
        private string usernameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                if ((object.ReferenceEquals(this.passwordField, value) != true)) {
                    this.passwordField = value;
                    this.RaisePropertyChanged("password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                if ((object.ReferenceEquals(this.usernameField, value) != true)) {
                    this.usernameField = value;
                    this.RaisePropertyChanged("username");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PlayerStatistics", Namespace="http://schemas.datacontract.org/2004/07/TheHiddenTreasuresWCF")]
    public partial class PlayerStatistics : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int coinsField;
        
        private int gamesPlayedField;
        
        private int gamesWonField;
        
        private int minTimeField;
        
        private string usernameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int coins {
            get {
                return this.coinsField;
            }
            set {
                if ((this.coinsField.Equals(value) != true)) {
                    this.coinsField = value;
                    this.RaisePropertyChanged("coins");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int gamesPlayed {
            get {
                return this.gamesPlayedField;
            }
            set {
                if ((this.gamesPlayedField.Equals(value) != true)) {
                    this.gamesPlayedField = value;
                    this.RaisePropertyChanged("gamesPlayed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int gamesWon {
            get {
                return this.gamesWonField;
            }
            set {
                if ((this.gamesWonField.Equals(value) != true)) {
                    this.gamesWonField = value;
                    this.RaisePropertyChanged("gamesWon");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int minTime {
            get {
                return this.minTimeField;
            }
            set {
                if ((this.minTimeField.Equals(value) != true)) {
                    this.minTimeField = value;
                    this.RaisePropertyChanged("minTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                if ((object.ReferenceEquals(this.usernameField, value) != true)) {
                    this.usernameField = value;
                    this.RaisePropertyChanged("username");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ValidateUser", ReplyAction="http://tempuri.org/IService1/ValidateUserResponse")]
        System.Threading.Tasks.Task<bool> ValidateUserAsync(TheHiddenTreasures.ServiceReference1.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RegisterUser", ReplyAction="http://tempuri.org/IService1/RegisterUserResponse")]
        System.Threading.Tasks.Task<bool> RegisterUserAsync(TheHiddenTreasures.ServiceReference1.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdateStatistics", ReplyAction="http://tempuri.org/IService1/UpdateStatisticsResponse")]
        System.Threading.Tasks.Task<bool> UpdateStatisticsAsync(string username, bool didWin, int time, int coins);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPlayerStatistics", ReplyAction="http://tempuri.org/IService1/GetPlayerStatisticsResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<TheHiddenTreasures.ServiceReference1.PlayerStatistics>> GetPlayerStatisticsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPlayerCoins", ReplyAction="http://tempuri.org/IService1/GetPlayerCoinsResponse")]
        System.Threading.Tasks.Task<int> GetPlayerCoinsAsync(string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : TheHiddenTreasures.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<TheHiddenTreasures.ServiceReference1.IService1>, TheHiddenTreasures.ServiceReference1.IService1 {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public Service1Client() : 
                base(Service1Client.GetDefaultBinding(), Service1Client.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IService1.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), Service1Client.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<bool> ValidateUserAsync(TheHiddenTreasures.ServiceReference1.User user) {
            return base.Channel.ValidateUserAsync(user);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterUserAsync(TheHiddenTreasures.ServiceReference1.User user) {
            return base.Channel.RegisterUserAsync(user);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateStatisticsAsync(string username, bool didWin, int time, int coins) {
            return base.Channel.UpdateStatisticsAsync(username, didWin, time, coins);
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<TheHiddenTreasures.ServiceReference1.PlayerStatistics>> GetPlayerStatisticsAsync() {
            return base.Channel.GetPlayerStatisticsAsync();
        }
        
        public System.Threading.Tasks.Task<int> GetPlayerCoinsAsync(string username) {
            return base.Channel.GetPlayerCoinsAsync(username);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IService1)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IService1)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:8733/Design_Time_Addresses/TheHiddenTreasuresWCF/Service1/");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return Service1Client.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IService1);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return Service1Client.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IService1);
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_IService1,
        }
    }
}

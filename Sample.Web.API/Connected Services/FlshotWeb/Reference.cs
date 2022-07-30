﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlshotWeb
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:cdc:iisb:2011", ConfigurationName="FlshotWeb.InterOp_ServiceSoap")]
    public interface InterOp_ServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cdc:iisb:2011:connectivityTestFL", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> connectivityTestFLAsync(string echoBack, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cdc:iisb:2011:submitSingleMessage", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> submitSingleMessageAsync(string username, string password, string facilityID, string hl7Message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface InterOp_ServiceSoapChannel : FlshotWeb.InterOp_ServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class InterOp_ServiceSoapClient : System.ServiceModel.ClientBase<FlshotWeb.InterOp_ServiceSoap>, FlshotWeb.InterOp_ServiceSoap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public InterOp_ServiceSoapClient() : 
                base(InterOp_ServiceSoapClient.GetDefaultBinding(), InterOp_ServiceSoapClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.HL7IISMethods.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public InterOp_ServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(InterOp_ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), InterOp_ServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public InterOp_ServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(InterOp_ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public InterOp_ServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(InterOp_ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public InterOp_ServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> connectivityTestFLAsync(string echoBack, string username, string password)
        {
            return base.Channel.connectivityTestFLAsync(echoBack, username, password);
        }
        
        public System.Threading.Tasks.Task<string> submitSingleMessageAsync(string username, string password, string facilityID, string hl7Message)
        {
            return base.Channel.submitSingleMessageAsync(username, password, facilityID, hl7Message);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.HL7IISMethods))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
                httpsBindingElement.AllowCookies = true;
                httpsBindingElement.MaxBufferSize = int.MaxValue;
                httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpsBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.HL7IISMethods))
            {
                return new System.ServiceModel.EndpointAddress("https://www.flshots.com/interop/InterOp.Service.HL7IISMethods.cls");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return InterOp_ServiceSoapClient.GetBindingForEndpoint(EndpointConfiguration.HL7IISMethods);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return InterOp_ServiceSoapClient.GetEndpointAddress(EndpointConfiguration.HL7IISMethods);
        }
        
        public enum EndpointConfiguration
        {
            
            HL7IISMethods,
        }
    }
}
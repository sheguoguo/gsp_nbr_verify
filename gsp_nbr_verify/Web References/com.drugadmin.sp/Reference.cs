﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行库版本:2.0.50727.1433
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 2.0.50727.1433 版自动生成。
// 
#pragma warning disable 1591

namespace gsp_nbr_verify.com.drugadmin.sp {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SuperPassPort", Namespace="http://www.cneport.com/webservices/superpass")]
    public partial class SuperPass : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback serviceOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SuperPass() {
            this.Url = global::gsp_nbr_verify.Properties.Settings.Default.gsp_nbr_verify_com_drugadmin_sp_SuperPass;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event serviceCompletedEventHandler serviceCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://www.cneport.com/webservices/superpass", ResponseNamespace="http://www.cneport.com/webservices/superpass")]
        [return: System.Xml.Serialization.SoapElementAttribute("result", DataType="base64Binary")]
        public byte[] service(string serviceName, [System.Xml.Serialization.SoapElementAttribute(DataType="base64Binary")] byte[] requestContext, [System.Xml.Serialization.SoapElementAttribute(DataType="base64Binary")] byte[] requestData, [System.Xml.Serialization.SoapElementAttribute(DataType="base64Binary")] out byte[] responseData) {
            object[] results = this.Invoke("service", new object[] {
                        serviceName,
                        requestContext,
                        requestData});
            responseData = ((byte[])(results[1]));
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void serviceAsync(string serviceName, byte[] requestContext, byte[] requestData) {
            this.serviceAsync(serviceName, requestContext, requestData, null);
        }
        
        /// <remarks/>
        public void serviceAsync(string serviceName, byte[] requestContext, byte[] requestData, object userState) {
            if ((this.serviceOperationCompleted == null)) {
                this.serviceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnserviceOperationCompleted);
            }
            this.InvokeAsync("service", new object[] {
                        serviceName,
                        requestContext,
                        requestData}, this.serviceOperationCompleted, userState);
        }
        
        private void OnserviceOperationCompleted(object arg) {
            if ((this.serviceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.serviceCompleted(this, new serviceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }

        internal byte[] service(string p, byte[] key_bytes, byte[] corp_bytes)
        {
            throw new NotImplementedException();
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    public delegate void serviceCompletedEventHandler(object sender, serviceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class serviceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal serviceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
        
        /// <remarks/>
        public byte[] responseData {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[1]));
            }
        }
    }
}

#pragma warning restore 1591
using System;
using System.Collections.Generic;
using System.Threading.Tasks;       // Task<T>
using System.Net.Http;              // need for HttpClient
using System.Text;                  // Encoding.UTF8
using ie.delegates.reactives;
using ie.exceptions;
using ie.extension.methods;


namespace ie.developments.Http
{

    public class IeHttpGetTask0 : IeAbsHttpGetTask<HttpResponseMessage> {

        public IeHttpGetTask0(
            HttpClient httpClient, 
            string url, 
            Action<HttpResponseMessage> successCallback, 
            Action<IeRuntimeException> errorCallback) : base(httpClient, url, successCallback, errorCallback) { }

        public IeHttpGetTask0(
            HttpClient httpClient,
            string url,
            IDictionary<string, string> header,
            Action<HttpResponseMessage> successCallback,
            Action<IeRuntimeException> errorCallback) : base(httpClient, url, header, successCallback, errorCallback) { }

        protected override async Task<HttpResponseMessage> runTaskAsync() {                      
            try {
                //await Task.Delay(10000, tokenSource.Token).ConfigureAwait(false);
                HttpResponseMessage response = await issueHttpRequest();                
                Console.WriteLine("{0} - StatusCode: {1}", this.getLogTag(), response.StatusCode);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Console.WriteLine("{0} - responseBody: {1}", this.getLogTag(), responseBody);
                return response;
            }
            catch (HttpRequestException cause) {
                Console.WriteLine("{0} - Error on httpClient.GetStringAsync: {1}", this.getLogTag(), cause.Message);
                throw new IeRuntimeException("Error on httpClient.GetStringAsync", cause, "12345");
            }
        }
    }

    public class IeHttpPostTask0 : IeAbsHttpPostTask<HttpResponseMessage> {

        public IeHttpPostTask0(
            HttpClient httpClient, 
            string url, 
            string jsonString, 
            Action<HttpResponseMessage> successCallback,
            Action<IeRuntimeException> errorCallback) : base(httpClient, url, jsonString, successCallback, errorCallback) { }

        public IeHttpPostTask0(
            HttpClient httpClient,
            string url,
            string jsonString,
            IDictionary<string, string> header,
            Action<HttpResponseMessage> successCallback,
            Action<IeRuntimeException> errorCallback) : base(httpClient, url, jsonString, header, successCallback, errorCallback) { }

        protected override async Task<HttpResponseMessage> runTaskAsync() {
            try {
                //await Task.Delay(10000, tokenSource.Token).ConfigureAwait(false);
                HttpResponseMessage response = await issueHttpRequest();
                Console.WriteLine("{0} - StatusCode: {1}", this.getLogTag(), response.StatusCode);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Console.WriteLine("{0} - responseBody: {1}", this.getLogTag(), responseBody);
                return response;
            }
            catch (HttpRequestException cause) {
                Console.WriteLine("{0} - Error on httpClient.GetStringAsync: {1}", this.getLogTag(), cause.Message);
                throw new IeRuntimeException("Error on httpClient.GetStringAsync", cause, "12345");
            }
        }
    }
    
    ///

    public abstract class IeAbsHttpGetTask<T> : AbsAsyncAwaitTask<T> {

        private readonly HttpClient httpClient;

        private readonly string url;

        private readonly IDictionary<string, string> header;
        
        public IeAbsHttpGetTask(HttpClient httpClient, string url, Action<T> successCallback, Action<IeRuntimeException> errorCallback) 
            : base(successCallback, errorCallback) {

            this.httpClient = httpClient;
            this.url = url;
            this.header = new Dictionary<string, string>();
        }

        public IeAbsHttpGetTask(
            HttpClient httpClient, 
            string url, 
            IDictionary<string, string> header, 
            Action<T> successCallback, 
            Action<IeRuntimeException> errorCallback) : base(successCallback, errorCallback) {

            this.httpClient = httpClient;
            this.url = url;
            Dictionary<string, string> tempHeader = new Dictionary<string, string>();
            if (null != header) { tempHeader.Append(header); }
            this.header = tempHeader;
        }

        protected override int timeout {
            get { return 5000; }
        }

        protected async Task<HttpResponseMessage> issueHttpRequest() {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, this.url);
            if (this.header.Count > 0) {
                foreach (KeyValuePair<string, string> kvp in this.header) {
                    requestMessage.Headers.Add(kvp.Key, kvp.Value);
                }
            }

            try {
                return await httpClient.SendAsync(requestMessage).ConfigureAwait(false);
            }
            catch (HttpRequestException cause) {
                Console.WriteLine("{0} - Error on httpClient.GetStringAsync: {1}", this.getLogTag(), cause.Message);
                throw new IeRuntimeException("Error on httpClient.GetStringAsync", cause, "12345");
            }
        }
    }

    public abstract class IeAbsHttpPostTask<T> : AbsAsyncAwaitTask<T> {

        private readonly HttpClient httpClient;

        private readonly string url;

        private readonly string jsonString;

        private readonly IDictionary<string, string> header;

        public IeAbsHttpPostTask(
            HttpClient httpClient, 
            string url, 
            string jsonString, 
            Action<T> successCallback, 
            Action<IeRuntimeException> errorCallback) : base(successCallback, errorCallback) {

            this.httpClient = httpClient;
            this.url = url;
            this.jsonString = jsonString;
            this.header = new Dictionary<string, string>();
        }

        public IeAbsHttpPostTask(
            HttpClient httpClient, 
            string url, 
            string jsonString, 
            IDictionary<string, string> header,
            Action<T> successCallback,
            Action<IeRuntimeException> errorCallback) : base(successCallback, errorCallback) {

            this.httpClient = httpClient;
            this.url = url;
            this.jsonString = jsonString;
            Dictionary<string, string> tempHeader = new Dictionary<string, string>();
            if (null != header) { tempHeader.Append(header); }
            this.header = tempHeader;
        }

        protected override int timeout {
            get { return 5000; }
        }

        protected async Task<HttpResponseMessage> issueHttpRequest() {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, this.url);
            if (this.header.Count > 0) {
                foreach (KeyValuePair<string, string> kvp in this.header) {
                    requestMessage.Headers.Add(kvp.Key, kvp.Value);
                }
            }

            requestMessage.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                
            try {                
                return await httpClient.SendAsync(requestMessage).ConfigureAwait(false);
            }
            catch (HttpRequestException cause) {
                Console.WriteLine("{0} - Error on httpClient.GetStringAsync: {1}", this.getLogTag(), cause.Message);
                throw new IeRuntimeException("Error on httpClient.GetStringAsync", cause, "12345");
            }
        }
    }
}
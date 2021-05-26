using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Common.Library
{
    public abstract class AbstractRestClient : IRestClient
    {
        /// <summary>
        /// Best practice: Make HttpClient static and reuse.
        /// Creating a new instance for each request is an antipattern that can result in socket exhaustion.
        /// </summary>
        protected readonly HttpClient httpClient;
        protected readonly IServiceLocator serviceLocator;

        /// <summary>
        /// Base constructor class.
        /// </summary>
        /// <param name="httpClient">The HttpClient.</param>
        /// <param name="serviceLocator">Service locator.</param>
        public AbstractRestClient(HttpClient httpClient, IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            this.httpClient = httpClient;
        }

        ///<inheritdoc/>
        public virtual async Task<TReturnMessage> GetAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path)
            where TReturnMessage : class, new()
        {
            HttpResponseMessage response;

            var uri = new Uri($"{this.serviceLocator.GetServiceUri(serviceName)}/{path}");
            // Here is actual call to target service
            this.ResetRestClientHeaders();
            response = await this.httpClient.GetAsync(uri).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var ex = new HttpRequestException($"{response.StatusCode} -- {response.ReasonPhrase}");

                // Stuff the Http StatusCode in the Data collection with key 'StatusCode'
                ex.Data.Add("StatusCode", response.StatusCode);
                throw ex;
            }

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

            return JsonConvert.DeserializeObject<TReturnMessage>(result);
        }

        ///<inheritdoc/>
        public virtual async Task<TReturnMessage> PostAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path, object dataObject = null)
            where TReturnMessage : class, new()
        {
            var uri = new Uri($"{this.serviceLocator.GetServiceUri(serviceName)}/{path}");
            string result = null;
            var content = dataObject != null ? JsonConvert.SerializeObject(dataObject) : "{}";

            using (StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
            {
                this.ResetRestClientHeaders();
                var response = await this.httpClient.PostAsync(uri, stringContent).ConfigureAwait(false);

                try
                {
                    response.EnsureSuccessStatusCode();

                    if (!response.IsSuccessStatusCode)
                    {
                        return await Task.FromResult(new TReturnMessage()).ConfigureAwait(false);
                    }

                    result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    var stop = ex.Message + " " + ex.InnerException;
                }

                return JsonConvert.DeserializeObject<TReturnMessage>(result);
            }
        }

        ///<inheritdoc/>
        public virtual async Task<TReturnMessage> PutAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path, object dataObject = null)
            where TReturnMessage : class, new()
        {
            var uri = new Uri($"{this.serviceLocator.GetServiceUri(serviceName)}/{path}");

            var content = dataObject != null ? JsonConvert.SerializeObject(dataObject) : "{}";

            using (StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
            {
                this.ResetRestClientHeaders();
                var response = await this.httpClient.PutAsync(uri, stringContent).ConfigureAwait(true);
                response.EnsureSuccessStatusCode();

                if (!response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(new TReturnMessage()).ConfigureAwait(true);
                }

                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                return JsonConvert.DeserializeObject<TReturnMessage>(result);
            }
        }

        ///<inheritdoc/>
        public virtual async Task<bool> DeleteAsync(ServiceLocatorDomain serviceName, string path)
        {
            var uri = new Uri($"{this.serviceLocator.GetServiceUri(serviceName)}/{path}");

            this.ResetRestClientHeaders();
            var response = await this.httpClient.DeleteAsync(uri).ConfigureAwait(true);
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        ///<inheritdoc/>
        public virtual async Task<TReturnMessage> PostAsync<TReturnMessage>(RestClientRequestOptions restClientRequestOptions)
            where TReturnMessage : class, new()
        {
            if (restClientRequestOptions == null)
            {
                throw new ArgumentNullException(nameof(restClientRequestOptions));
            }

            var serviceName = restClientRequestOptions.ServiceName;
            var path = restClientRequestOptions.Path;
            var dataObject = restClientRequestOptions.DataObject;

            var uri = new Uri($"{this.serviceLocator.GetServiceUri(serviceName)}/{path}");

            var content = dataObject != null ? JsonConvert.SerializeObject(dataObject) : "{}";

            using (StringContent stringContent = new StringContent(content, Encoding.UTF8, restClientRequestOptions.ParameterContentType))
            {
                this.ResetRestClientHeaders();
                var response = await this.httpClient.PostAsync(uri, stringContent).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                if (!response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(new TReturnMessage()).ConfigureAwait(false);
                }

                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<TReturnMessage>(result);
            }
        }

        /// <summary>
        /// This method cleans the http request headers before every REST call.
        /// </summary>
        protected virtual void ResetRestClientHeaders()
        {
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

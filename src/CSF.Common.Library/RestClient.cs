namespace CSF.Common.Library
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using CSF.Common.Library.Azure;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    /// <summary>
    /// Rest client that can be used by the application to make API calls.
    /// </summary>
    public class RestClient : IRestClient
    {
        /// <summary>
        /// Best practice: Make HttpClient static and reuse.
        /// Creating a new instance for each request is an antipattern that can result in socket exhaustion.
        /// </summary>
        private readonly HttpClient httpClient;
        private readonly IServiceLocator serviceLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HttpClient.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <param name="serviceLocator">Service locator.</param>
        /// <param name="azureKeyVaultService">Azure Key Vault instance for the application.</param>
        public RestClient(HttpClient httpClient, IConfiguration configuration, IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Makes a GET call to the specified API.
        /// </summary>
        /// <typeparam name="TReturnMessage">Object type returned by the API.</typeparam>
        /// <param name="serviceName">Name of the service as specified by <see cref="ServiceLocatorDomain"/>.</param>
        /// <param name="path">Path to the API being called on the service.</param>
        /// <returns>Return bject as specified by the API.</returns>
        public async Task<TReturnMessage> GetAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path)
            where TReturnMessage : class, new()
        {
            HttpResponseMessage response;

            var uri = new Uri($"{this.serviceLocator.GetServiceUri(serviceName)}/{path}");

            // Here is actual call to target service
            this.ResetRestClientHeaders();
            response = await this.httpClient.GetAsync(uri).ConfigureAwait(true);

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

        /// <summary>
        /// Makes a POST call to the specified API.
        /// </summary>
        /// <typeparam name="TReturnMessage">Object type returned by the API.</typeparam>
        /// <param name="serviceName">Name of the service as specified by <see cref="ServiceLocatorDomain"/>.</param>
        /// <param name="path">Path to the API being called on the service.</param>
        /// <param name="dataObject">Object to post to the API.</param>
        /// <returns>Return object as specified by the API.</returns>
        public async Task<TReturnMessage> PostAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path, object dataObject = null)
            where TReturnMessage : class, new()
        {
            var uri = new Uri($"{this.serviceLocator.GetServiceUri(serviceName)}/{path}");

            var content = dataObject != null ? JsonConvert.SerializeObject(dataObject) : "{}";

            using (StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
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
        /// Makes a PUT call to the specified API.
        /// </summary>
        /// <typeparam name="TReturnMessage">Object type returned by the API.</typeparam>
        /// <param name="serviceName">Name of the service as specified by <see cref="ServiceLocatorDomain"/>.</param>
        /// <param name="path">Path to the API being called on the service.</param>
        /// <param name="dataObject">Object to put to the API.</param>
        /// <returns>Return bject as specified by the API.</returns>
        public async Task<TReturnMessage> PutAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path, object dataObject = null)
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

        /// <summary>
        /// Makes a DELETE call to the specified API.
        /// </summary>
        /// <param name="serviceName">Name of the service as specified by <see cref="ServiceLocatorDomain"/>.</param>
        /// <param name="path">Path to the API being called on the service.</param>
        /// <returns>A boolean value representing result of operation.</returns>
        public async Task<bool> DeleteAsync(ServiceLocatorDomain serviceName, string path)
        {
            var uri = new Uri($"{this.serviceLocator.GetServiceUri(serviceName)}/{path}");

            this.ResetRestClientHeaders();
            var response = await this.httpClient.DeleteAsync(uri).ConfigureAwait(true);
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        private void ResetRestClientHeaders()
        {
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Makes a POST call to the specified API.
        /// </summary>
        /// <typeparam name="TReturnMessage">Object type returned by the API.</typeparam>
        /// <param name="restClientRequestOptions">the rest client request options.</param>
        /// <returns>Return object as specified by the API.</returns>
        public async Task<TReturnMessage> PostAsync<TReturnMessage>(RestClientRequestOptions restClientRequestOptions)
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

    }
}
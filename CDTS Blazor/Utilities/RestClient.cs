namespace CDNApplication.Utilities
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using CDNApplication.Data.Services;
    using Newtonsoft.Json;

    public class RestClient : IRestClient
    {
        /// <summary>
        /// Best practice: Make HttpClient static and reuse.
        /// Creating a new instance for each request is an antipattern that can result in socket exhaustion.
        /// </summary>
        private static readonly HttpClient _client;

        /// <summary>
        /// Api key for MTOA.
        /// </summary>
        private readonly string _mtoaApiKey;

        /// <summary>
        /// JWT token for MTOA API.
        /// </summary>
        private readonly string _mtoaJwtToken;

        private readonly IServiceLocator _serviceLocator;

        static RestClient()
        {
            _client = new HttpClient(); 
        }

        public RestClient(IServiceLocator serviceLocator, AzureKeyVaultService azureKeyVaultService)
        {
            this._serviceLocator = serviceLocator;
            this._mtoaApiKey = azureKeyVaultService.GetSecretByName("MtoaApiKey");
            this._mtoaJwtToken = azureKeyVaultService.GetSecretByName("MtoaJwt");

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.TryAddWithoutValidation("app-jwt", this._mtoaJwtToken);
            _client.DefaultRequestHeaders.TryAddWithoutValidation("api-key", this._mtoaApiKey);
        }

        public async Task<TReturnMessage> GetAsync<TReturnMessage>(ServiceDomain serviceName, string path)
            where TReturnMessage : class, new()
        {
            HttpResponseMessage response;

            var uri = new Uri($"{_serviceLocator.GetServiceUri(serviceName)}/{path}");

            // Here is actual call to target service              
            response = await _client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                var ex = new HttpRequestException($"{response.StatusCode} -- {response.ReasonPhrase}");
                
                // Stuff the Http StatusCode in the Data collection with key 'StatusCode'
                ex.Data.Add("StatusCode", response.StatusCode);
                throw ex;
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TReturnMessage>(result);
        }

        public async Task<TReturnMessage> PostAsync<TReturnMessage>(ServiceDomain serviceName, string path, object dataObject = null)
            where TReturnMessage : class, new()

        {
            var uri = new Uri($"{_serviceLocator.GetServiceUri(serviceName)}/{path}");

            var content = dataObject != null ? JsonConvert.SerializeObject(dataObject) : "{}";

            var response = await _client.PostAsync(uri, new StringContent(content, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                return await Task.FromResult(new TReturnMessage());
            }

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TReturnMessage>(result);
        }

        public async Task<TReturnMessage> PutAsync<TReturnMessage>(ServiceDomain serviceName, string path, object dataObject = null)
            where TReturnMessage : class, new()
        {
            var uri = new Uri($"{_serviceLocator.GetServiceUri(serviceName)}/{path}");

            var content = dataObject != null ? JsonConvert.SerializeObject(dataObject) : "{}";

            var response = await _client.PutAsync(uri, new StringContent(content, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                return await Task.FromResult(new TReturnMessage());
            }

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TReturnMessage>(result);
        }

        public async Task<bool> DeleteAsync(ServiceDomain serviceName, string path)
        {
            var uri = new Uri($"{_serviceLocator.GetServiceUri(serviceName)}/{path}");

            var response = await _client.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
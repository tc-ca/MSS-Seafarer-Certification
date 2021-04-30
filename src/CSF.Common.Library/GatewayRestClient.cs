using CSF.Common.Library.Azure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Common.Library
{
    public class GatewayRestClient : IGatewayRestClient
    {
        private readonly HttpClient httpClient;
        private readonly IServiceLocator serviceLocator;

        public GatewayRestClient(HttpClient httpClient, IConfiguration configuration, IServiceLocator serviceLocator, IKeyVaultService keyVaultService)
        {

            this.serviceLocator = serviceLocator;
            this.httpClient = httpClient;
            var secretName = configuration.GetSection("AzureKeyVaultSettings")["SecretNames:GatewayToken"];

            var token = keyVaultService.GetSecretByName(secretName);
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        }


        //public void Search(ApplicantSearchCriteria searchCriteria)
        //{

        //}

        public async Task<TReturnMessage> GetAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path) where TReturnMessage : class, new()
        {
            HttpResponseMessage response;

            var uri = new Uri($"{this.serviceLocator.GetServiceUri(serviceName)}/{path}");


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



    }
}

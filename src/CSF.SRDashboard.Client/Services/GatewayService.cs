using CSF.Common.Library.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CSF.SRDashboard.Client.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<GatewayService> logger;
        private  HttpClient httpClient;
        private  IKeyVaultService keyVaultService;

        public GatewayService( IConfiguration configuration, ILogger<GatewayService> logger, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient();
            this.configuration = configuration;
            var gatewayServiceUrl = configuration.GetSection("ServiceLocatorEndpoints")["GatewayToMPDIS"];
            this.httpClient.BaseAddress = new Uri(gatewayServiceUrl);
            this.logger = logger;
        }

        public void SetKeyVault(IKeyVaultService keyVaultService)
        {
            this.keyVaultService = keyVaultService;
            var secretName = configuration.GetSection("AzureKeyVaultSettings")["SecretNames:GatewayToken"];
            var token = keyVaultService.GetSecretByName(secretName);
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public ApplicantPersonalInfo GetApplicantInfoByCdn(string cdn)
        {
            ApplicantPersonalInfo aplicantPeronalInfo = null;
            HttpResponseMessage response;
            string requestUri = this.httpClient.BaseAddress + $"Applicant/{cdn}";

            try
            {
                response = this.httpClient.GetAsync(requestUri).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    aplicantPeronalInfo = JsonConvert.DeserializeObject<ApplicantPersonalInfo>(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return aplicantPeronalInfo;
        }


        public ApplicantSearchResult Search(ApplicantSearchCriteria searchCriteria)
        {
            ApplicantSearchResult searchResult = null;
            string requestAction = "search";
            var serializedContent = JsonConvert.SerializeObject(searchCriteria);
            StringContent stringContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");

            try
            {
                var response = this.httpClient.PostAsync(requestAction, stringContent).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    searchResult = JsonConvert.DeserializeObject<ApplicantSearchResult>(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return searchResult;
        }
    }
}

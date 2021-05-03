using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CSF.Common.Library
{
    /// <summary>
    /// This IRestClient implements security necessary to make valid calls against our custom API gateway.
    /// </summary>
    public class GatewayRestClient : AbstractRestClient
    {
        private readonly string bearerToken;

        public GatewayRestClient(HttpClient httpClient, IServiceLocator serviceLocator, IConfiguration appConfiguration) : base(httpClient, serviceLocator)
        {
            this.bearerToken = appConfiguration.GetSection("AzureKeyVaultSettings")["SecretNames:GatewayToken"];
        }

        /// <summary>
        /// Override to insert bearer token into our calls.
        /// </summary>
        protected override void ResetRestClientHeaders()
        {
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.bearerToken);
        }
    }
}

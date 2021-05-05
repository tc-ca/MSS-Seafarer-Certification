using Microsoft.Extensions.Configuration;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayRestClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HttpClient.</param>
        /// <param name="serviceLocator">Service locator.</param>
        public GatewayRestClient(HttpClient httpClient, IServiceLocator serviceLocator, IConfiguration appConfiguration) : base(httpClient, serviceLocator)
        {
            this.bearerToken = appConfiguration.GetSection("AzureKeyVaultSettings")["SecretNames:GatewayToken"];
        }

        /// <summary>
        /// Override to insert bearer token into our calls.
        /// </summary>
        protected override void ResetRestClientHeaders()
        {
            base.ResetRestClientHeaders();

            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.bearerToken);
        }
    }
}

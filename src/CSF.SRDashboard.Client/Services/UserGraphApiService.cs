using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services
{
    /// <summary>
    /// Service class to retrieve user information from Graph API.
    /// </summary>
    public class UserGraphApiService : IUserGraphApiService
    {
        private readonly IConfiguration configuration;
        private HttpClient httpClient;
        private ITokenAcquisition tockenAcquisition;
        private MicrosoftIdentityConsentAndConditionalAccessHandler consentHandler;

        public UserGraphApiService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ITokenAcquisition tokenAcquisitionService, MicrosoftIdentityConsentAndConditionalAccessHandler consentHandler)
        {
            this.configuration = configuration;
            this.httpClient = httpClientFactory.CreateClient();
            this.tockenAcquisition = tokenAcquisitionService;
            this.consentHandler = consentHandler;
        }

        /// <summary>
        /// Returns user's display name, otherwise returns <code="null"/>.
        /// </summary>
        /// <returns></returns>
        public string GetUserDisplayName()
        {
            this.acquireToken();

            string userDisplayName = null;
            try
            {
                string url = this.configuration.GetSection("AzureGraphAPI")["UserProfileURL"];
                var basicInfoRequest = this.httpClient.GetAsync(url).GetAwaiter().GetResult();
                if (basicInfoRequest.IsSuccessStatusCode)
                {
                    var userData = System.Text.Json.JsonDocument.Parse(basicInfoRequest.Content.ReadAsStreamAsync().GetAwaiter().GetResult());
                    userDisplayName = userData.RootElement.GetProperty("displayName").GetString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return userDisplayName;
        }

        /// <summary>
        /// Returns user's display photo if available, otherwise returns <code="null"/>.
        /// </summary>
        /// <returns></returns>
        public string GetUserPhotoData()
        {
            this.acquireToken();

            string photoDataString = string.Empty;
            try
            {
                string url = this.configuration.GetSection("AzureGraphAPI")["UserPhotoURL"];
                var photoRequest = this.httpClient.GetAsync(url).GetAwaiter().GetResult();
                if (photoRequest.IsSuccessStatusCode)
                {
                    var photoData = photoRequest.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                    var byteArray = photoData.ToArray();
                    string base64String = Convert.ToBase64String(byteArray);
                    photoDataString = "data:image/jpg;base64," + base64String;
                }
                else
                {
                    photoDataString = Constants.NoProfilePicturePath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return photoDataString;
        }

        /// <summary>
        /// Acquires a valid token for calling Graph API. If token is invalid, instruct <see cref="consentHandler"/></cref> to refresh it.
        /// See https://github.com/AzureAD/microsoft-identity-web/wiki/Managing-incremental-consent-and-conditional-access.
        /// </summary>
        /// <returns></returns>
        private void acquireToken()
        {
            try
            {
                var token = this.tockenAcquisition.GetAccessTokenForUserAsync(new string[] { "User.Read" }).GetAwaiter().GetResult();
                this.httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.InnerException);
                consentHandler.HandleException(ex);
            }
        }
    }
}

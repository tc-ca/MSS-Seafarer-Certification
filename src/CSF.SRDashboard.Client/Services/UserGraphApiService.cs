using CSF.SRDashboard.Client.DTO.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

            string photoDataString = null;
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


        //TODO: clean up
        public List<AzureMemberInfo> GetMmeGroupMembers()
        {
            List<AzureMemberInfo> groupMembers = new List<AzureMemberInfo>();

            try
            {
                // following gets all the users . Under current Active directory               
                string url = "https://graph.microsoft.com/v1.0/users";

                //string groupId = "744b2679-64c3-4bd3-be2d-4b684cbf411a";  // this is from the test group on my personal Azure AD

                //CSF Marine Medical Users (MME): 0b8dac87-f006-414a-8f9a-a1d689756c2e
                string groupId = "0b8dac87-f006-414a-8f9a-a1d689756c2e";

                string url2 = $"https://graph.microsoft.com/v1.0/groups/{groupId}/members/microsoft.graph.user";

                var basicInfoRequest = this.httpClient.GetAsync(url2).GetAwaiter().GetResult();

                if (basicInfoRequest.IsSuccessStatusCode)
                {
                    var userData = System.Text.Json.JsonDocument.Parse(basicInfoRequest.Content.ReadAsStreamAsync().GetAwaiter().GetResult());
                    var rootElement = userData.RootElement.ToString();
                    var memberList = JsonConvert.DeserializeObject<AzureMemberListInfo>(rootElement);
                    var members = memberList.value;
                    groupMembers = members.OrderBy(x => x.surname).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return groupMembers;
        }


        //TODO: clean up
        public List<AzureMemberInfo> GetGroupMembersByGroupId(string groupId)
        {
            List<AzureMemberInfo> groupMembers = new List<AzureMemberInfo>();

            try
            {
                // following gets all the users . Under current Active directory               
                string url = "https://graph.microsoft.com/v1.0/users";

                //test group id
                //string groupId = "744b2679-64c3-4bd3-be2d-4b684cbf411a";

                groupId = "744b2679-64c3-4bd3-be2d-4b684cbf411a";
                string url2 = $"https://graph.microsoft.com/v1.0/groups/{groupId}/members/microsoft.graph.user";

                var basicInfoRequest = this.httpClient.GetAsync(url2).GetAwaiter().GetResult();

                if (basicInfoRequest.IsSuccessStatusCode)
                {
                    var userData = System.Text.Json.JsonDocument.Parse(basicInfoRequest.Content.ReadAsStreamAsync().GetAwaiter().GetResult());
                    var rootElement = userData.RootElement.ToString();
                    var memberList = JsonConvert.DeserializeObject<AzureMemberListInfo>(rootElement);
                    groupMembers = memberList.value;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return groupMembers;
        }

        public AzureMemberInfo GetUserByUserId(string Id)
        {

            string url = $"https://graph.microsoft.com/v1.0/users/{Id}";

            AzureMemberInfo memberInfo = null;
            try
            {
                var userRequest = this.httpClient.GetAsync(url).GetAwaiter().GetResult();
                if (userRequest.IsSuccessStatusCode)
                {
                    var userData = System.Text.Json.JsonDocument.Parse(userRequest.Content.ReadAsStreamAsync().GetAwaiter().GetResult());
                    var rootElement = userData.RootElement.ToString();
                    memberInfo = JsonConvert.DeserializeObject<AzureMemberInfo>(rootElement);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return memberInfo;
        }
    }
}

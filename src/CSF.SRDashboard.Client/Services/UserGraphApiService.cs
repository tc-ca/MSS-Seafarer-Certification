using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using System;
using System.Linq;
using System.Net.Http;


namespace CSF.SRDashboard.Client.Services
{
    public class UserGraphApiService : IUserGraphApiService
    {
        private readonly IConfiguration configuration;
        private HttpClient httpClient;

        public UserGraphApiService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ITokenAcquisition tokenAcquisitionService)
        {
            this.configuration = configuration;
            this.httpClient = httpClientFactory.CreateClient();
            var token = tokenAcquisitionService.GetAccessTokenForUserAsync(new string[] { "User.Read" }).GetAwaiter().GetResult();
            this.httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public string GetUserDisplayName()
        {
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

        public string GetUserPhotoData()
        {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return photoDataString;
        }
    }
}

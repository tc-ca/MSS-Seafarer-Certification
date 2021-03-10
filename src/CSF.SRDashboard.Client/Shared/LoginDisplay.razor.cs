using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Shared
{
    public partial class LoginDisplay
    {
       [Inject]
        IHttpClientFactory HttpClientFactory { get; set; }

        [Inject]
        Microsoft.Identity.Web.ITokenAcquisition TokenAcquisitionService { get; set; }
        private HttpClient _httpClient;
        private string userDisplayName;
        private string photoDataString;

        protected override void OnInitialized()
        {
            _httpClient = HttpClientFactory.CreateClient();

            var token = TokenAcquisitionService.GetAccessTokenForUserAsync(new string[] { "User.Read"}).GetAwaiter().GetResult();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            
            var basicInfoRequest = _httpClient.GetAsync("https://graph.microsoft.com/beta/me").GetAwaiter().GetResult();
            if (basicInfoRequest.IsSuccessStatusCode)
            {
                var userData = System.Text.Json.JsonDocument.Parse(basicInfoRequest.Content.ReadAsStreamAsync().GetAwaiter().GetResult());
                userDisplayName = userData.RootElement.GetProperty("displayName").GetString();
            }

            var photoRequest = _httpClient.GetAsync("https://graph.microsoft.com/v1.0/me/photo/$value").GetAwaiter().GetResult();

            if (photoRequest.IsSuccessStatusCode)
            {
                var photoData = photoRequest.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                var byteArray= photoData.ToArray();
                string base64String = Convert.ToBase64String(byteArray);
                photoDataString = "data:image/jpg;base64," + base64String;
            }
        }
    }
}

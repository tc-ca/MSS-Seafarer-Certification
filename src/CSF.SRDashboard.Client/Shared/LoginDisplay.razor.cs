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


        //@inject IHttpClientFactory HttpClientFactory
        //@inject Microsoft.Identity.Web.ITokenAcquisition TokenAcquisitionService

       [Inject]
        IHttpClientFactory HttpClientFactory { get; set; }

        [Inject]
        Microsoft.Identity.Web.ITokenAcquisition TokenAcquisitionService { get; set; }
        private HttpClient _httpClient;
        private string userDisplayName;
        protected override void OnInitialized()
        {
            _httpClient = HttpClientFactory.CreateClient();
            var token = TokenAcquisitionService.GetAccessTokenForUserAsync(new string[] { "User.Read", "Mail.Read" }).GetAwaiter().GetResult();

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var dataRequest = _httpClient.GetAsync("https://graph.microsoft.com/beta/me").GetAwaiter().GetResult();

            if (dataRequest.IsSuccessStatusCode)
            {
                var userData = System.Text.Json.JsonDocument.Parse(dataRequest.Content.ReadAsStreamAsync().GetAwaiter().GetResult());
                userDisplayName = userData.RootElement.GetProperty("displayName").GetString();
            }

            var photoRequest =  _httpClient.GetAsync("https://graph.microsoft.com/v1.0/me/photo/$value").GetAwaiter().GetResult();

            if (photoRequest.IsSuccessStatusCode)
            {
                var userData = System.Text.Json.JsonDocument.Parse(photoRequest.Content.ReadAsStreamAsync().GetAwaiter().GetResult());
                //var photoDataString = userData.RootElement.GetProperty("displayName").GetString();
            }
        }
    }
}

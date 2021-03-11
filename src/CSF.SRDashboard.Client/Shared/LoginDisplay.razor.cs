using CSF.SRDashboard.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;


namespace CSF.SRDashboard.Client.Shared
{
    public partial class LoginDisplay
    {
        private string userDisplayName;
        private string photoDataString;

        [Inject]
        protected IGraphApiService graphService { get; set; }

        protected override void OnInitialized()
        {
            userDisplayName = graphService.GetUserDisplayName();
            photoDataString = graphService.GetUserPhotoData();
        }
    }
}

using CSF.SRDashboard.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CSF.SRDashboard.Client.Shared
{
    public partial class LoginDisplay
    {
        [Inject] AuthenticationStateProvider authenticationStateProvider { get; set; }
        [Inject] IUserGraphApiService graphApiService { get; set; }

        private string userDisplayName;
        private string photoDataString;


        protected override async void OnInitialized()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
                this.userDisplayName = graphApiService.GetUserDisplayName();
                this.photoDataString = graphApiService.GetUserPhotoData();
                var groupMembers = graphApiService.GetMmeGroupMembers();
            }
        }
    }
}

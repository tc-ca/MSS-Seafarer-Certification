using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CSF.SRDashboard.Client.Shared
{
    public partial class LoginDisplay
    {
        [Inject] AuthenticationStateProvider authenticationStateProvider { get; set; }
        [Inject] IUserGraphApiService graphApiService { get; set; }

        [Inject] SessionState State { get; set; }

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
                this.State.LoggedInUser = this.userDisplayName;
                var groupMembers = graphApiService.GetMarineMedicalStaffMembers();
            }
        }
    }
}

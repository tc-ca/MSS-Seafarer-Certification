using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using System.Threading.Tasks;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.PageValidators;
using CSF.SRDashboard.Client.Components.Icons.Constants;
using CSF.SRDashboard.Client.Components.Icons.Utilities;
namespace CSF.SRDashboard.Client.Pages
{
    public partial class CreateNewRequest
    {
        protected EditContext EditContext;

        [Parameter]
        public string Cdn { get; set; }

        public string Comment { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        [Inject]
        public IWorkLoadManagementService WorkLoadService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        public RequestModel RequestModel { get; set; }


        public RequestValidator validator = new RequestValidator();

        public WorkItemDTO UploadedRequest { get; set; }

        public bool MostRecentCommentsIsCollapsed { get; private set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.Applicant = new MpdisApplicantDto();

            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);

            RequestModel = new RequestModel
            {
                Cdn = Applicant.Cdn
            };
            var t = FontAwesomeIconSize.TWO;
            var t1 = FontAwesomeSpinAnimationType.SPIN;
            this.EditContext = new EditContext(RequestModel);

            StateHasChanged();
        }

        public void SaveChanges()
        {
            var isValid = EditContext.Validate();
            if (!isValid)
            {
                return;
            }

            var RequestToSend = new RequestModel
            {
                Cdn = Applicant.Cdn,
                CertificateType = Constants.CertificateTypes.Where(x => x.ID.Equals(RequestModel.CertificateType)).Single().Text,
                RequestType = Constants.RequestTypes.Where(x => x.ID.Equals(RequestModel.RequestType)).Single().Text,
                SubmissionMethod = Constants.SubmissionMethods.Where(x => x.ID.Equals(RequestModel.SubmissionMethod)).Single().Text
            };

            UploadedRequest = WorkLoadService.PostRequestModel(RequestToSend, GatewayService);

            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "/" + UploadedRequest.Id);
        }

        private void SetMostRecentCommentsCollapseState()
        {
            MostRecentCommentsIsCollapsed = !MostRecentCommentsIsCollapsed;
        }

        public void ViewProfile()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn);
        }
    }
}

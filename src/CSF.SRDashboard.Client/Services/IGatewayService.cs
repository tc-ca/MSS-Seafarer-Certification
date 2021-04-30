using CSF.Common.Library.Azure;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;

namespace CSF.SRDashboard.Client.Services
{
    public interface IGatewayService
    {
        ApplicantPersonalInfo GetApplicantInfoByCdn(string cdn);
        ApplicantSearchResult Search(ApplicantSearchCriteria searchCriteria);
        void SetKeyVault(IKeyVaultService keyVaultService);
    }
}
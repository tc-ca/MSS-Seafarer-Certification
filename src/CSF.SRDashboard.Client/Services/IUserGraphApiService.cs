
using CSF.SRDashboard.Client.DTO.Azure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services
{
    public interface IUserGraphApiService
    {
        public string GetUserDisplayName();
        public string GetUserPhotoData();
        List<AzureMemberInfo> GetMarineMedicalStaffMembers();
        AzureMemberInfo GetUserByUserId(string Id);
    }
}

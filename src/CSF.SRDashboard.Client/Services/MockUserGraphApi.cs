using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services
{
    public class MockUserGraphApi : IUserGraphApiService
    {
        public string GetUserDisplayName()
        {
            return "Default";
        }

        public string GetUserPhotoData()
        {
            return "https://www.minervastrategies.com/wp-content/uploads/2016/03/default-avatar.jpg";
        }
    }
}

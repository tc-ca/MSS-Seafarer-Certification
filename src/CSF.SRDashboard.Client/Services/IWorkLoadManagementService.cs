using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services
{
    public interface IWorkLoadManagementService
    {
        WorkItemDTO GetByWorkItemById(int id);
        List<WorkItemDTO> GetByLineOfBusinessId(string lineOfBusinessId);
        WorkItemDTO AddWorkItem(WorkItemDTO workItem);
        List<WorkItemDTO> GetByCdnNumber(string cdn);
        WorkItemDTO PostRequestModel(RequestModel requestModel, IGatewayService gatewayService);
    }
}

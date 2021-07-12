using CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities;
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
        /// <summary>
        /// Gets a work item by the given work item id in Work Load Management Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a WorkItemDTO</returns>
        WorkItemDTO GetByWorkItemById(int id);

        /// <summary>
        /// Gets a list of work items by the given line of business id in Work Load Management Service
        /// </summary>
        /// <param name="lineOfBusinessId"></param>
        /// <returns>a list of WorkItemDTO</returns>
        List<WorkItemDTO> GetByLineOfBusinessId(string lineOfBusinessId);

        /// <summary>
        /// Adds a work item into the Work Load Management Service when passing a workItem DTO
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns>the newly added WorkItem dto with an id assigned to it</returns>
        WorkItemDTO AddWorkItem(WorkItemDTO workItem);

        /// <summary>
        /// Gets a list of work items from Work Load Management Service by a given CDN number of a seafarer
        /// </summary>
        /// <param name="cdn"></param>
        /// <returns>a list of WorkItemDTO</returns>
        List<WorkItemDTO> GetByCdnNumber(string cdn);

        /// <summary>
        /// Makes a post request and creates a work item inside Work Load Management Service from RequestModel
        /// RequestModel represents the data on the Create Request page. We also need to use a Gateway service to get Applicant information from MPDIS
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="gatewayService"></param>
        /// <returns>the newly created WorkItem in Workload Management Service</returns>
        WorkItemDTO PostRequestModel(RequestModel requestModel, IGatewayService gatewayService);

        /// <summary>
        /// Updates a work item after retrieving data from the request model.
        /// RequestModel represents the data on the Create Request/Edit page. We also need to use a Gateway service to get Applicant information from MPDIS
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="gatewayService"></param>
        /// <returns></returns>
        WorkItemDTO UpdateWorkItemForRequestModel(RequestModel requestModel, IGatewayService gatewayService);

        /// <summary>
        /// Updates a work item in Work Load Management Service
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns>returns the updated work item if successful, returns null if the call fails</returns>
        WorkItemDTO UpdateWorkitem(WorkItemDTO workItem);


        /// <summary>
        /// Gets work items from Work Load Management Service and formats them into WorkloadRequestTableItem
        /// This method is used for passing data and populating WorkLoadRequestTable
        /// </summary>
        /// <param name="cdn"></param>
        /// <returns>List of WorkloadRequestTableItem</returns>
        List<WorkloadRequestTableItem> GetByCdnInRequestTableFormat(string cdn);

        List<WorkloadRequestTableItem> GetAllInRequestTableFormat();

        Task<WorkItemAttachmentDTO> AddWorkItemAttachment(WorkItemAttachmentDTO workItemAttachmentDTO);
        List<WorkItemAttachmentDTO> GetAllAttachmentsByRequestId(int workitemId);


        WorkItemStatusDTO AddWorkItemStatus(WorkItemStatusDTO status);
        List<WorkItemStatusDTO> GetWorkItemStatuses(int workItemId);
        int DeleteOrPost(WorkItemAssignmentDTO assignment, bool isToDelete);
        WorkItemAssignmentDTO GetAssignmentFromRequestModel(RequestModel request);
        WorkItemAssignmentDTO GetMostRecentAssingmentForWorkItem(int workItemId);

    }
}

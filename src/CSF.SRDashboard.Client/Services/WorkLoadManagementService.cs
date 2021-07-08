namespace CSF.SRDashboard.Client.Services
{
    using CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities;
    using CSF.SRDashboard.Client.DTO;
    using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
    using CSF.SRDashboard.Client.Models;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.Extensions.Logging;
    using Microsoft.Identity.Web;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class WorkLoadManagementService : IWorkLoadManagementService
    {
        private AuthenticationStateProvider authenticationStateProvider;
        private IDownstreamWebApi downstreamWebApi;
        private readonly ILogger<WorkLoadManagementService> logger;

        public WorkLoadManagementService(IDownstreamWebApi downstreamWebApi, AuthenticationStateProvider authenticationStateProvider, ILogger<WorkLoadManagementService> logger)
        {
            this.downstreamWebApi = downstreamWebApi;
            this.authenticationStateProvider = authenticationStateProvider;
            this.logger = logger;
        }

        public async Task<ClaimsPrincipal> GetUserClaims()
        {
            var authState = await this.authenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User;
        }

        public async Task<WorkItemDTO> GetByWorkItemById(int id)
        {
            WorkItemDTO workItem = null;

            string requestPath = $"api/v1/workitems/{id}";

            if (id == 0)
                return null;

            try
            {
                workItem = await this.downstreamWebApi.GetForUserAsync<WorkItemDTO>("ApiWorkManagement", requestPath, user: await this.GetUserClaims());
                //workItem = this.restClient.GetAsync<WorkItemDTO>(ServiceLocatorDomain.WorkLoadManagement, requestPath).GetAwaiter().GetResult();

                if (!string.IsNullOrWhiteSpace(workItem.Detail))
                {
                    workItem.ItemDetail = JsonSerializer.Deserialize<WorkItemDetail>(workItem.Detail);
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return workItem;
        }

        public async Task<List<WorkItemDTO>> GetByLineOfBusinessId(string lineOfBusinessId)
        {
            List<WorkItemDTO> workItems = null;

            string requestPath = $"api/v1/workitems/lineofbusinesses/{lineOfBusinessId}/workitems";

            if (string.IsNullOrEmpty(lineOfBusinessId))
                return workItems;

            try
            {
                workItems = await this.downstreamWebApi.GetForUserAsync<List<WorkItemDTO>>("ApiWorkManagement", requestPath, user: await this.GetUserClaims());
                //workItems = this.restClient.GetAsync<List<WorkItemDTO>>(ServiceLocatorDomain.WorkLoadManagement, requestPath).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return workItems;
        }

        public async Task<WorkItemDTO> AddWorkItem(WorkItemDTO workItem)
        {
            string requestPath = $"api/v1/workitems";

            try
            {
                workItem = await this.downstreamWebApi.PostForUserAsync<WorkItemDTO, WorkItemDTO>("ApiWorkManagement", requestPath, workItem, user: await this.GetUserClaims());
                //workItem = this.restClient.PostAsync<WorkItemDTO>(ServiceLocatorDomain.WorkLoadManagement, requestPath, workItem).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return workItem;
        }

        public async Task<WorkItemDTO> UpdateWorkitem(WorkItemDTO workItem)
        {
            WorkItemDTO updatedWorkItem = null;
            string requestPath = $"api/v1/workitems";
            try
            {
                updatedWorkItem = await this.downstreamWebApi.PutForUserAsync<WorkItemDTO, WorkItemDTO>("ApiWorkManagement", requestPath, workItem, user: await this.GetUserClaims());
                //var result = this.restClient.UpdateAsync(ServiceLocatorDomain.WorkLoadManagement, requestPath, workItem).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return updatedWorkItem;
        }

        public async Task<List<WorkItemDTO>> GetByCdnNumber(string cdn)
        {
            List<WorkItemDTO> workItems = new List<WorkItemDTO>();
            string requestPath = $"api/v1/workitems/applicants/ContactUniqueId/{cdn}/workitems";
            if (string.IsNullOrEmpty(cdn))
                return workItems;

            try
            {
                workItems = await this.downstreamWebApi.GetForUserAsync<List<WorkItemDTO>>("ApiWorkManagement", requestPath, user: await this.GetUserClaims());
                //workItems = this.restClient.GetAsync<List<WorkItemDTO>>(ServiceLocatorDomain.WorkLoadManagement, requestPath).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return workItems;
        }

        public async Task<WorkItemAttachmentDTO> AddWorkItemAttachment(WorkItemAttachmentDTO workItemAttachmentDTO)
        {
            string requestPath = "/api/v1/workitem-attachments";
            try
            {
                return await this.downstreamWebApi.PostForUserAsync<WorkItemAttachmentDTO, WorkItemAttachmentDTO>("ApiWorkManagement", requestPath, workItemAttachmentDTO, user: await this.GetUserClaims());
                //return await this.restClient.PostAsync<WorkItemAttachmentDTO>(ServiceLocatorDomain.WorkLoadManagement, requestPath, workItemAttachmentDTO);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }
            return new WorkItemAttachmentDTO();
        }

        public async Task<List<WorkItemAttachmentDTO>> GetAllAttachmentsByRequestId(int workitemId)
        {
            string requestPath = $"/api/v1/workitem-attachments/{workitemId}/attachments";
            try
            {
                return await this.downstreamWebApi.GetForUserAsync<List<WorkItemAttachmentDTO>>("ApiWorkManagement", requestPath, user: await this.GetUserClaims());
                //return this.restClient.GetAsync<List<WorkItemAttachmentDTO>>(ServiceLocatorDomain.WorkLoadManagement, requestPath).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }
            return new List<WorkItemAttachmentDTO>();
        }

        public async Task<WorkItemStatusDTO> AddWorkItemStatus(WorkItemStatusDTO status)
        {
            WorkItemStatusDTO updatedStatus = null;
            string requestPath = $"api/v1/workitems/statuses";
            try
            {
                updatedStatus = await this.downstreamWebApi.PostForUserAsync<WorkItemStatusDTO, WorkItemStatusDTO>("ApiWorkManagement", requestPath, status, user: await this.GetUserClaims());
                //updatedStatus = this.restClient.PostAsync<WorkItemStatusDTO>(ServiceLocatorDomain.WorkLoadManagement, requestPath, status).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return updatedStatus;
        }

        /// <summary>
        /// Gets all requests 
        /// </summary>
        /// <returns>All requests in workload table format</returns>
        public async Task<List<WorkloadRequestTableItem>> GetAllInRequestTableFormat()
        {
            var workItems = await this.GetByLineOfBusinessId(Constants.MarineMedical);
            return PopulateWorkloadItem(workItems);
        }

        /// <summary>
        /// gets all requests linked to a given CDN
        /// </summary>
        /// <param name="cdn"></param>
        /// <returns>Requests linked to CDN in workload table format</returns>
        public async Task<List<WorkloadRequestTableItem>> GetByCdnInRequestTableFormat(string cdn)
        {
            var workItems = await this.GetByCdnNumber(cdn);
            return PopulateWorkloadItem(workItems);
        }

        /// <summary>
        /// Populates the work items into the Workload request item for the frontend table
        /// </summary>
        /// <param name="workItems"></param>
        /// <returns></returns>
        private List<WorkloadRequestTableItem> PopulateWorkloadItem(List<WorkItemDTO> workItems)
        {
            List<WorkloadRequestTableItem> tableItems = new List<WorkloadRequestTableItem>();
            if (workItems != null)
            {
                foreach (var workItem in workItems)
                {
                    var tableItem = new WorkloadRequestTableItem();

                    if (workItem.Detail != null)
                    {
                        var detail = JsonSerializer.Deserialize<WorkItemDetail>(workItem.Detail);
                        tableItem.Certificate = detail.CertificateType;
                        tableItem.RequestType = detail.RequestType;
                        tableItem.ApplicantCDN = detail.Cdn;
                        tableItem.ProcessingPhase = detail.ProcessingPhase;
                    }
             
                    tableItem.RequestId = workItem.Id.ToString();

                    tableItem.RequestDate = workItem.CreatedDateUTC?.DateTime;
                    if (workItem.WorkItemStatus != null)
                    {
                        tableItem.Status = workItem.WorkItemStatus.StatusAdditionalDetails;
                    }

                    if(workItem.WorkItemAssignment != null)
                    {
                        tableItem.AssigneeId = workItem.WorkItemAssignment.AssignedEmployeeId;
                    }
                    tableItems.Add(tableItem);
                }
            }
                return tableItems;
            }
        
        public async Task<WorkItemDTO> PostRequestModel(RequestModel requestModel, IGatewayService gatewayService)
        {
            var Cdn = requestModel.Cdn;
            var applicantInfo = gatewayService.GetApplicantInfoByCdn(Cdn);
            
            // -- Contact
            var contact = this.GetContacInfoDtoFromApplicant(applicantInfo, true, null);

            //--Assignment
            var assignment = this.GetAssignmentFromRequestModel(requestModel);

            //-- Detail
            WorkItemDetail itemDetail = new WorkItemDetail();
            itemDetail.RequestType = requestModel.RequestType;
            itemDetail.CertificateType = requestModel.CertificateType;
            itemDetail.SubmissionMethod = requestModel.SubmissionMethod;
            itemDetail.ApplicantName = requestModel.ApplicantFullName;
            itemDetail.Cdn = requestModel.Cdn;
            itemDetail.ProcessingPhase = requestModel.ProcessingPhase;
            itemDetail.HasAttachments = (requestModel.UploadedDocuments != null) ? true : false;
            itemDetail.Comments = requestModel.Comments;
            itemDetail.Assignment = assignment;
            string itemDetailString = JsonSerializer.Serialize(itemDetail);

            WorkItemDTO workItem = new WorkItemDTO();
            workItem.InitialDetailJson = itemDetailString;
            workItem.Detail = itemDetailString;

            workItem.ApplicantContact = contact;
            workItem.CreatedDateUTC = DateTime.UtcNow;
            workItem.ReceivedDateUTC = DateTime.UtcNow;
            workItem.LastUpdatedDateUTC = DateTime.UtcNow;

            workItem.SameApplicantSubmitterInd = true;
            workItem.LineOfBusinessId = Constants.MarineMedical;
            // WorkItemStatuses
            workItem.WorkItemStatus = new WorkItemStatusDTO();
            workItem.WorkItemStatus.StatusAdditionalDetails = requestModel.Status;

            workItem.WorkItemAssignment = assignment;
            var uploadedWorkItem = await this.AddWorkItem(workItem);

            return uploadedWorkItem;
        }

        public async Task<WorkItemDTO> UpdateWorkItemForRequestModel(RequestModel requestModel, IGatewayService gatewayService)
        {
            int requestId = Convert.ToInt32(requestModel.RequestID);
            var existingWorkItem = await this.GetByWorkItemById(requestId);

            var cdn = requestModel.Cdn;
            var applicantInfo = gatewayService.GetApplicantInfoByCdn(cdn);

            var contact = this.GetContacInfoDtoFromApplicant(applicantInfo, false, existingWorkItem);
            var itemDetailString = this.GetItemDetailFromRequestModel(requestModel);

            WorkItemDTO workItem = new WorkItemDTO();
            workItem.Id = requestModel.RequestID ;
            workItem.Detail = itemDetailString;
            workItem.ApplicantContact = contact;
            workItem.CreatedDateUTC = existingWorkItem.CreatedDateUTC;
            workItem.ReceivedDateUTC = existingWorkItem.ReceivedDateUTC;
            workItem.LastUpdatedDateUTC = DateTime.UtcNow;
            workItem.SameApplicantSubmitterInd = true;
            workItem.LineOfBusinessId = Constants.MarineMedical;

            //Assignment 
            workItem.WorkItemAssignment = this.GetAssignmentFromRequestModel(requestModel);

            // WorkItemStatuses
            workItem.WorkItemStatus = new WorkItemStatusDTO();
            workItem.WorkItemStatus.StatusAdditionalDetails = requestModel.Status;
            workItem.WorkItemStatus.WorkItemId = requestModel.RequestID;
            this.AddWorkItemStatus(workItem.WorkItemStatus);
            var uploadedWorkItem = await this.UpdateWorkitem(workItem);

            return uploadedWorkItem;
        }

        private ContactInformationDTO GetContacInfoDtoFromApplicant(MpdisApplicantDto applicant, bool isNewContact, WorkItemDTO exitingWorkItem)
        {
            ContactInformationDTO contact = new ContactInformationDTO();
            if (isNewContact)
            {
                contact.Id = Guid.NewGuid().ToString();
            }
            else
            {
                contact.Id = exitingWorkItem.ApplicantContact.Id;

                var t = applicant.Id;
                var to = applicant.ContactId;
            }

            contact.Name = applicant.FullName;
            contact.AddressLine1 = applicant.HomeAddress;
            contact.City = applicant.HomeAddressCity;
            contact.Province = applicant.HomeAddressProvince;
            contact.Country = applicant.HomeAddressCountry;
            contact.PostalCode = applicant.HomeAddressPostalCode;
            contact.Phone = applicant.PhoneNumber;
            contact.Email = applicant.Email;
            contact.PrimaryContactInd = true;
            contact.ContactUniqueId = applicant.Cdn;

            return contact;
        }

        private string GetItemDetailFromRequestModel(RequestModel requestModel)
        {
            WorkItemDetail itemDetail = new WorkItemDetail();
            itemDetail.RequestType = requestModel.RequestType;
            itemDetail.CertificateType = requestModel.CertificateType;
            itemDetail.SubmissionMethod = requestModel.SubmissionMethod;
            itemDetail.ApplicantName = requestModel.ApplicantFullName;
            itemDetail.Cdn = requestModel.Cdn;
            itemDetail.ProcessingPhase = requestModel.ProcessingPhase;
            itemDetail.HasAttachments = (requestModel.UploadedDocuments != null) ? true : false;
            itemDetail.Comments = requestModel.Comments;
            itemDetail.Assignment = this.GetAssignmentFromRequestModel(requestModel);
            string itemDetailString = JsonSerializer.Serialize(itemDetail);

            return itemDetailString;
        }

        public WorkItemAssignmentDTO GetAssignmentFromRequestModel(RequestModel request)
        {
            WorkItemAssignmentDTO assignee = null;

            assignee = new WorkItemAssignmentDTO();
            if (request.RequestID > 0)
            {
                assignee.WorkItemId = request.RequestID;
            }
            assignee.AssignedEmployeeId = request.AssigneeId;
            assignee.DateAssignedUTC = DateTimeOffset.UtcNow;

            return assignee;
        }
  
        public WorkItemAssignmentDTO PostAssignmentForWorkItem(WorkItemAssignmentDTO assignment)
        {
            WorkItemAssignmentDTO uploadedAssignment=null;
            //
            string requestPath = "api/v1/workitems/assignments";

            try
            {
                uploadedAssignment = this.restClient.PostAsync<WorkItemAssignmentDTO>(ServiceLocatorDomain.WorkLoadManagement, requestPath, assignment).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return uploadedAssignment;
        }

        /// <summary>
        /// Deletes an assignment from work load service management by assignment id
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns> Returns zero if the deletion is successful. 
        /// if Work load management service returns a failure, this method returns -1. If an exception thrown within this method, this method returns 0</returns>
        public int DeleteAssignment(int assignmentId)
        {
            int deletedInd = 0;
            string requestPath = $"api/v1/workitems/assignments/{assignmentId}";

            try
            {
                var isDeleted =this.restClient.DeleteAsync(ServiceLocatorDomain.WorkLoadManagement, requestPath).GetAwaiter().GetResult();
                deletedInd = (isDeleted ? 1 : -1);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return deletedInd;
        }

        /// <summary>
        /// This method either posts or deletes an assignment depending on the parameter isToDelete
        /// </summary>
        /// <param name="assignment"></param>
        /// <param name="isToDelete"></param>
        /// <returns></returns>
        public int DeleteOrPost(WorkItemAssignmentDTO assignment, bool isToDelete)
        {
            int updateResult = 0;

            if(isToDelete)
            {
                var deletedIndicator = this.DeleteAssignment(assignment.Id);
                updateResult = (deletedIndicator > 0 ? 1 : -1 );
            }
            else
            {
                var uploadedAssignment = this.PostAssignmentForWorkItem(assignment);
                updateResult = (uploadedAssignment == null ? -1 : 1); 
            }
            return updateResult;
        }

        /// <summary>
        /// This method gets the only valid Assignment for the work item. There is only one valid assignee at any given time.
        /// When the assignee changes, the old assignee is set to be deleted and a new assignee is inserted on the Work_Item_Assignment table
        /// </summary>
        /// <param name="workItemId"></param>
        /// <returns>the currently associated Assignment to the workItemId</returns>
        public WorkItemAssignmentDTO GetMostRecentAssingmentForWorkItem(int workItemId)
        {
            List<WorkItemAssignmentDTO> assignments = new List<WorkItemAssignmentDTO>();
            WorkItemAssignmentDTO assignment = null;
            string requestPath = $"api/v1/workitems/{workItemId}/assignments";
            try
            {
                assignments = this.restClient.GetAsync<List<WorkItemAssignmentDTO>>(ServiceLocatorDomain.WorkLoadManagement, requestPath).GetAwaiter().GetResult();
                assignment = assignments.OrderBy(x => x.Id).Last();// most recent assignment for the workItem
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return assignment;
        }
    }
}

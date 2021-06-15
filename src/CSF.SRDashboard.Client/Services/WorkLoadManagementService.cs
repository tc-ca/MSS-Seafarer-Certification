using CSF.Common.Library;
using CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services
{
    public class WorkLoadManagementService : IWorkLoadManagementService
    {
        private readonly IRestClient restClient;
        private readonly ILogger<WorkLoadManagementService> logger;

        public WorkLoadManagementService(IEnumerable<IRestClient> restClientCollection, ILogger<WorkLoadManagementService> logger)
        {
            this.restClient = restClientCollection.First(o => o is UnauthenticatedRestClient);
            this.logger = logger;
        }

        public WorkItemDTO GetByWorkItemById(int id)
        {
            WorkItemDTO workItem = null;

            string requestPath = $"api/v1/workitems/{id}";

            if (id == 0)
                return null;

            try
            {
                workItem = this.restClient.GetAsync<WorkItemDTO>(ServiceLocatorDomain.WorkLoadManagement, requestPath).GetAwaiter().GetResult();

                if (!string.IsNullOrWhiteSpace(workItem.InitialDetailJson))
                {
                    workItem.ItemDetail = JsonSerializer.Deserialize<WorkItemDetail>(workItem.InitialDetailJson);
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return workItem;
        }

        public List<WorkItemDTO> GetByLineOfBusinessId(string lineOfBusinessId)
        {
            List<WorkItemDTO> workItems = null;

            string requestPath = $"api/v1/workitems/lineofbusinesses/{lineOfBusinessId}/workitems";

            if (string.IsNullOrEmpty(lineOfBusinessId))
                return workItems;

            try
            {
                workItems = this.restClient.GetAsync<List<WorkItemDTO>>(ServiceLocatorDomain.WorkLoadManagement, requestPath).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return workItems;
        }

        public WorkItemDTO AddWorkItem(WorkItemDTO workItem)
        {
            string requestPath = $"api/v1/workitems";

            try
            {
                workItem = this.restClient.PostAsync<WorkItemDTO>(ServiceLocatorDomain.WorkLoadManagement, requestPath, workItem).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return workItem;
        }

        public WorkItemDTO UpdateWorkitem(WorkItemDTO workItem)
        {
            WorkItemDTO updatedWorkItem = null;
            string requestPath = $"api/v1/workitems";
            try
            {
                var result = this.restClient.UpdateAsync(ServiceLocatorDomain.WorkLoadManagement, requestPath, workItem).GetAwaiter().GetResult();
                if (result > 0)
                {
                    updatedWorkItem = this.GetByWorkItemById(workItem.Id);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return updatedWorkItem;
        }

        public List<WorkItemDTO> GetByCdnNumber(string cdn)
        {
            List<WorkItemDTO> workItems = new List<WorkItemDTO>();
            string requestPath = $"api/v1/workitems/applicants/ContactUniqueId/{cdn}/workitems";
            if (string.IsNullOrEmpty(cdn))
                return workItems;

            try
            {
                workItems = this.restClient.GetAsync<List<WorkItemDTO>>(ServiceLocatorDomain.WorkLoadManagement, requestPath).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return workItems;
        }
        /// <summary>
        /// Gets all requests 
        /// </summary>
        /// <returns>All requests in workload table format</returns>
        public List<WorkloadRequestTableItem> GetAllInRequestTableFormat()
        {
            var workItems = this.GetByLineOfBusinessId(Constants.MarineMedical);
            return PopulateWorkloadItem(workItems);
        }
        /// <summary>
        /// gets all requests linked to a given CDN
        /// </summary>
        /// <param name="cdn"></param>
        /// <returns>Requests linked to CDN in workload table format</returns>
        public List<WorkloadRequestTableItem> GetByCdnInRequestTableFormat(string cdn)
        {
            var workItems = this.GetByCdnNumber(cdn);
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
                        
                    }
             
                    tableItem.RequestId = workItem.Id.ToString();
                    tableItem.RequestDate = workItem.CreatedDateUTC != null ? workItem.CreatedDateUTC.Value.DateTime : null;
                    if (workItem.WorkItemStatus != null)
                    {
                        tableItem.Status = workItem.WorkItemStatus.StatusAdditionalDetails;
                    }
                    tableItems.Add(tableItem);
                }
            }
                return tableItems;
            }
        

        
        public WorkItemDTO PostRequestModel(RequestModel requestModel, IGatewayService gatewayService)
        {
            var Cdn = requestModel.Cdn;
            var applicantInfo = gatewayService.GetApplicantInfoByCdn(Cdn);
            
            // -- Contact
            var contact = this.GetContacInfoDtoFromApplicant(applicantInfo, true, null);

            //-- Detail
            WorkItemDetail itemDetail = new WorkItemDetail();
            itemDetail.RequestType = requestModel.RequestType;
            itemDetail.CertificateType = requestModel.CertificateType;
            itemDetail.SubmissionMethod = requestModel.SubmissionMethod;
            itemDetail.ApplicantName = requestModel.ApplicantFullName;
            itemDetail.Cdn = requestModel.Cdn;
            itemDetail.HasAttachments = (requestModel.Documents != null) ? true : false;
            itemDetail.Comments = requestModel.Comments;
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
            workItem.WorkItemStatus.StatusAdditionalDetails = Constants.New;
            var uploadedWorkItem = this.AddWorkItem(workItem);

            return uploadedWorkItem;
        }
        public async Task<WorkItemAttachmentDTO> AddWorkItemAttachment(WorkItemAttachmentDTO workItemAttachmentDTO)
        {
            string requestPath = "/api/v1/workitem-attachments";
            try
            {
                return await this.restClient.PostAsync<WorkItemAttachmentDTO>(ServiceLocatorDomain.WorkLoadManagement, requestPath, workItemAttachmentDTO);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }
            return new WorkItemAttachmentDTO();
        }
        public List<WorkItemAttachmentDTO> GetAllAttachmentsByRequestId(int workitemId)
        {
            string requestPath = $"/api/v1/workitem-attachments/{workitemId}/attachments";
            try
            {   
                 return this.restClient.GetAsync<List<WorkItemAttachmentDTO>>(ServiceLocatorDomain.WorkLoadManagement, requestPath).GetAwaiter().GetResult();
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }
            return new List<WorkItemAttachmentDTO>();
        }
        public WorkItemDTO UpdateWorkItemForRequestModel(RequestModel requestModel, IGatewayService gatewayService)
        {
            int requestId = Convert.ToInt32(requestModel.RequestID);
            var existingWorkItem = this.GetByWorkItemById(requestId);

            var cdn = requestModel.Cdn;
            var applicantInfo = gatewayService.GetApplicantInfoByCdn(cdn);

            var contact = this.GetContacInfoDtoFromApplicant(applicantInfo, false, existingWorkItem);
            var itemDetailString = this.GetItemDetailFromRequestModel(requestModel);

            WorkItemDTO workItem = new WorkItemDTO();
            workItem.Id = requestModel.RequestID ;
            workItem.InitialDetailJson = itemDetailString;
            workItem.Detail = itemDetailString;
            workItem.ApplicantContact = contact;
            workItem.CreatedDateUTC = existingWorkItem.CreatedDateUTC;
            workItem.ReceivedDateUTC = existingWorkItem.ReceivedDateUTC;
            workItem.LastUpdatedDateUTC = DateTime.UtcNow;
            workItem.SameApplicantSubmitterInd = true;
            workItem.LineOfBusinessId = Constants.MarineMedical;
            // WorkItemStatuses
            workItem.WorkItemStatus = new WorkItemStatusDTO();
            workItem.WorkItemStatus.StatusAdditionalDetails = Constants.New;
            var uploadedWorkItem = this.UpdateWorkitem(workItem);

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
            itemDetail.HasAttachments = (requestModel.Documents != null) ? true : false;
            itemDetail.Comments = requestModel.Comments;
            string itemDetailString = JsonSerializer.Serialize(itemDetail);

            return itemDetailString;
        }


    }

}

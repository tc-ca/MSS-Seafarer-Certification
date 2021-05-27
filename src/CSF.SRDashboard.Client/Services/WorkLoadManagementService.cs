using CSF.Common.Library;
using CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities;
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
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return workItem;
        }

        public List<WorkItemDTO> GetByLineOfBusinessId(string lineOfBusinessId)
        {
            List<WorkItemDTO> workItems = new List<WorkItemDTO>();

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


        public List<WorkloadRequestTableItem> GetByCdnInRequestTableFormat(string cdn)
        {
            List<WorkloadRequestTableItem> tableItems = new List<WorkloadRequestTableItem>();

            var workItems = this.GetByCdnNumber(cdn);

            foreach (var workItem in workItems)
            {
                var tableItem = new WorkloadRequestTableItem();

                if (workItem.Detail != null)
                {
                    var detail = JsonSerializer.Deserialize<WorkItemDetail>(workItem.Detail);
                    tableItem.Certificate = detail.CertificateType;
                    tableItem.RequestType = detail.RequestType;
                }

                tableItem.RequestId = workItem.Id.ToString();
                tableItem.RequestDate = workItem.CreatedDateUTC.Value.DateTime;
                tableItem.Status = workItem.WorkItemStatus.StatusAdditionalDetails;

                tableItems.Add(tableItem);
            }

            return tableItems;
        }

        public WorkItemDTO PostRequestModel(RequestModel requestModel, IGatewayService gatewayService)
        {
            var Cdn = requestModel.Cdn;
            var applicantInfo = gatewayService.GetApplicantInfoByCdn(Cdn);

            // -- Contact
            ContactInformationDTO contact = new ContactInformationDTO();
            contact.Id = Guid.NewGuid().ToString();
            contact.Name = applicantInfo.FullName;
            contact.AddressLine1 = applicantInfo.HomeAddress;
            contact.City = applicantInfo.HomeAddressCity;
            contact.Province = applicantInfo.HomeAddressProvince;
            contact.Country = applicantInfo.HomeAddressCountry;
            contact.PostalCode = applicantInfo.HomeAddressPostalCode;
            contact.Phone = applicantInfo.PhoneNumber;
            contact.Email = applicantInfo.Email;
            contact.PrimaryContactInd = true;
            contact.ContactUniqueId = requestModel.Cdn;

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
            workItem.InitialDetail = itemDetailString;
            workItem.Detail = itemDetailString;

            workItem.ApplicantContact = contact;
            workItem.ReceivedDateUTC = DateTime.Now;
            workItem.LastUpdatedDateUTC = DateTime.Now;

            workItem.SameApplicantSubmitterInd = true;
            workItem.LineOfBusinessId = Constants.MarineMedical;
            // WorkItemStatuses
            workItem.WorkItemStatus = new WorkItemStatusDTO();
            workItem.WorkItemStatus.StatusAdditionalDetails = Constants.New;
            var uploadedWorkItem = this.AddWorkItem(workItem);

            return uploadedWorkItem;
        }
    }

}

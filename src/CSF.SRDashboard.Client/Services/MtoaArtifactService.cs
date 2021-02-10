using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSF.Web.Client.Utilities;
using System.Net.Http;

namespace CSF.SRDashboard.Client.Services
{
    public class MtoaArtifactService : IMtoaArtifactService
    {
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly ILogger<MtoaArtifactService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MtoaServices"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration service.</param>
        /// <param name="restClient">REST client service.</param>
        /// <param name="logger">Application logger service.</param>
        public MtoaArtifactService(IConfiguration configuration, IRestClient restClient, ILogger<MtoaArtifactService> logger)
        {
            this.configuration = configuration;
            this.restClient = restClient;
            this.logger = logger;
        }

        public int GetNumberOfRequestForSeafarers()
        {
            int numberOfRequest = -1;

            bool excludeMetadata = true;
            bool excludeServiceRequests = true;
            int page = 1;
            int pageSize = 1;

            string serviceId = this.configuration.GetSection("MtoaServiceSettings")["ServiceId"];
            string pathTemplate = this.configuration.GetSection("MtoaServiceSettings")["GetServiceRequestsPath"];
            string path = string.Format(pathTemplate, serviceId, page, pageSize, excludeMetadata, excludeServiceRequests);

            var serviceRequestInfo = restClient.GetAsync<ServiceRequestCollection>(ServiceLocatorDomain.Mtoa, path).GetAwaiter().GetResult();
            numberOfRequest = serviceRequestInfo.Count;

            return numberOfRequest;
        }

        public List<ServiceRequest> GetAllRequestsForSeafarers(int numberOfRequest)
        {
            List<ServiceRequest> allServiceRequests = null;

            bool excludeMetadata = true;
            bool excludeServiceRequests = false;
            int page = 1;
            int pageSize = numberOfRequest;
            string serviceId = this.configuration.GetSection("MtoaServiceSettings")["ServiceId"];
            string pathTemplate= this.configuration.GetSection("MtoaServiceSettings")["GetServiceRequestsPath"];
            string path = string.Format(pathTemplate,serviceId, page, pageSize, excludeMetadata, excludeServiceRequests);
            try
            {
                var serviceRequestCollection = restClient.GetAsync<ServiceRequestCollection>(ServiceLocatorDomain.Mtoa, path).GetAwaiter().GetResult();
                allServiceRequests = serviceRequestCollection.ServiceRequests.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return allServiceRequests;
        }

        public SeafarersArtifactDTO GetArtifactByServiceRequestId(int serviceRequestId)
        {
            SeafarersArtifactDTO artifactInfo = null;
            //string serviceId = this.configuration.GetSection("MtoaServiceSettings")["ServiceId"];
            string pathTemplate = this.configuration.GetSection("MtoaServiceSettings")["GetArtifactByServiceRequestIdPath"];

            string path = string.Format(pathTemplate, serviceRequestId);

            try
            {
                artifactInfo = restClient.GetAsync<SeafarersArtifactDTO>(ServiceLocatorDomain.Mtoa, path).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return artifactInfo;
        }

        public DashboardRow GetDashboarRowFromServiceRequest(ServiceRequest serviceRequest)
        {
            DashboardRow row = null;
            var serviceRequestId = serviceRequest.Id;

            var artifact = this.GetArtifactByServiceRequestId(serviceRequestId);

            if (artifact != null)
            {
                row = new DashboardRow();
                row.ServiceRequestNumber = serviceRequestId;
                // names will be assigned later.
                row.RequestType = artifact.CertificateType;
                row.CDN = artifact.CdnNumber;
                row.RequestType = artifact.CertificateType;
                row.AssignedTo = artifact.PersonAssignedTo;
                row.ProcessingPhase = serviceRequest.RequestStatus.ToString();
            }

            return row;
        }


        public SeafarersArtifactDTO GetArtifactByArtifactId(int artifactId)
        {
            SeafarersArtifactDTO artifact = null;
            string pathTemplate = this.configuration.GetSection("MtoaServiceSettings")["GetArtifactByArtifactIdPath"];

            string path = string.Format(pathTemplate, artifactId.ToString());

            try
            {
                artifact = restClient.GetAsync<SeafarersArtifactDTO>(ServiceLocatorDomain.Mtoa, path).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return artifact;
        }

        public List<DashboardRow> GetDashboardRowsInParallel()
        {
            List<DashboardRow> dashbaordRows = new List<DashboardRow>();
            var numberOfTotalRequests = GetNumberOfRequestForSeafarers();
            var allRequests = this.GetAllRequestsForSeafarers(numberOfTotalRequests);

            List<Task<DashboardRow>> multipleTasks = new List<Task<DashboardRow>>();

            foreach (var request in allRequests)
            {
                multipleTasks.Add(Task.Run(() => this.GetDashboarRowFromServiceRequest(request)));

                if (multipleTasks.Count > 10)
                {
                    var multipleTasksProgress = Task.WhenAll(multipleTasks);
                    try
                    {
                        multipleTasksProgress.Wait();
                        var multiTasksResult = multipleTasksProgress.Result;
                        dashbaordRows.AddRange(multiTasksResult.ToList());
                        multipleTasks.Clear();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            if (multipleTasks.Count > 0)
            {
                var taskProgress = Task.WhenAll(multipleTasks);
                try
                {
                    taskProgress.Wait();
                    var taskResult = taskProgress.Result;
                    dashbaordRows.AddRange(taskResult.ToList());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var rowsWithoutNull = dashbaordRows.OfType<DashboardRow>().OrderBy(x => x.ServiceRequestNumber).ToList();
            return rowsWithoutNull;
        }

    }
}

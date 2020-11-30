﻿// <auto-generated>
// added auto-generated to suppress the warning we will visit this part later.
// </auto-generated>

using CDNApplication.Data.DTO.MTAPI;
using CDNApplication.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CDNApplication.Data.Services
{
    public class MtoaRequestService: IMtoaRequestService
    {
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly ILogger<MtoaRequestService> logger;
        public MtoaRequestService(IConfiguration configuration, IRestClient restClient, ILogger<MtoaRequestService> logger)
        {
            this.configuration = configuration;
            this.restClient = restClient;
            this.logger = logger;
        }

        public int GetServiceRequestIdFromMTOA()
        {
            int serviceRequestId = -1;
            // this.configuration.GetSection("MtoaServiceSettings")["SeafarerCertificationServiceName"]

            string userId = this.configuration.GetSection("MtoaServiceSettings")["UserId"];
            string serviceId = this.configuration.GetSection("MtoaServiceSettings")["ServiceId"];
            string serviceNameEnglish = this.configuration.GetSection("MtoaServiceSettings")["ServiceNameInEnglish"];
            string serviceNameFrench = this.configuration.GetSection("MtoaServiceSettings")["ServiceNameInFrench"];
            string serviceRequestStatus = this.configuration.GetSection("MtoaServiceSettings")["ProgressStatus"];

            string path = String.Format("api/v1/servicerequests?userId={0}&serviceId={1}&englishName={2}&frenchName={3}&serviceRequestStatus={4}", 
                                        userId, serviceId, serviceNameEnglish, serviceNameFrench, serviceRequestStatus);

            try
            {
                
                HttpResponseMessage response = this.restClient.PostAsync(ServiceLocatorDomain.Mtoa, path).GetAwaiter().GetResult();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = HttpContentExtensions.ReadAsAsync<ServiceRequestCreationResult>(response.Content).GetAwaiter().GetResult();
                    serviceRequestId = result.ServiceRequestId;
                }

            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message +"\r\n"+ ex.InnerException);
            }

            return serviceRequestId;
        }

    }

}

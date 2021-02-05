﻿// <auto-generated>
// added auto-generated to suppress the warning we will visit this part later.
// </auto-generated>
using CSF.Web.Client.Data.DTO;
using CSF.Web.Client.Data.DTO.MTAPI;
using CSF.Web.Client.Models.PageModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSF.Web.Client.Data.Services
{
    public class MtoaArtifactService
    {

        private readonly string baseURL = "https://wwwappstest.tc.gc.ca/Saf-Sec-Sur/13/MTAPI-INT/api/v1/"; //This is for Dev only works within organizational network.
        private readonly MtoaFileService _mtoaFileService;
        private readonly IConfiguration configuration;

        private string api_key;
        private string jwt;

        public MtoaArtifactService(IKeyVaultService azureKeyVaultService, MtoaFileService mtoaFileService, IConfiguration configuration)
        {
            api_key = azureKeyVaultService.GetSecretByName("MtoaApiKey");
            jwt = azureKeyVaultService.GetSecretByName("MtoaJwt");
            _mtoaFileService = mtoaFileService;
            this.configuration = configuration;

        }

        private SeafarersArtifactDTO GetArtifactDTOfromPageModel(UploadDocumentPageModel model)
        {

            SeafarersArtifactDTO dto = new SeafarersArtifactDTO
            {
                CdnNumber = model.CdnNumber,
                CertificateType = model.CertificateType,
                ConfirmationNumber = model.ConfirmationNumber,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                SubmissionType = model.SubmissionType.ToString(),
                UploadedFilesInfo = new List<MtoaFileInfo>()
            };

            return dto;
        }

        public void PostPageModelToMtoaAsJSON(UploadDocumentPageModel model)
        {

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            int serviceRequestId = int.Parse(this.configuration.GetSection("MtoaServiceSettings")["ServiceRequestId"]);
            
            int userId = int.Parse(this.configuration.GetSection("MtoaServiceSettings")["UserId"]);

            var dto = GetArtifactDTOfromPageModel(model);

            // Upload files
            foreach (var x in model.UploadedFiles)
            {
                //var uploadedFile = _mtoaFileService.UploadFile(serviceRequestId, x.MtoaFileAttachment).GetAwaiter().GetResult();
                
                // Temporary solution
                Random random = new Random();

                dto.UploadedFilesInfo.Add(new MtoaFileInfo
                {
                    FileId = random.Next(0, 10000),
                    FileDescription = x.Description
                });
            }

            var json = JsonConvert.SerializeObject(dto);

            HttpClient client = new HttpClient();
            string baseURL = "https://wwwappstestext.tc.gc.ca/Saf-Sec-Sur/13/MTAPI-INT/api/";
            string path = string.Format("v1/artifacts?artifactType={0}&version={1}&serviceRequestId={2}&userId={3}",
                ArtifactType.JsonDocument.ToString(), 1, serviceRequestId.ToString(), userId.ToString());

            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("app-jwt", jwt);
            client.DefaultRequestHeaders.TryAddWithoutValidation("api-key", api_key);

            client.BaseAddress = new Uri(baseURL);

            var response = client.PostAsJsonAsync<string>(path, json).GetAwaiter().GetResult();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Can't save data.");
            }

        }

    }
}

﻿// <auto-generated>
// added auto-generated to suppress the warning we will visit this part later.
// </auto-generated>
using BlazorInputFile;
using CDNApplication.Data.DTO.MTAPI;
using CDNApplication.Exceptions;
using CDNApplication.Models.PageModels;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CDNApplication.Data.Services
{
    public class MtoaFileService
    {

        private readonly string baseURL = "https://wwwappstest.tc.gc.ca/Saf-Sec-Sur/13/MTAPI-INT/api/v1/"; //This is for Dev only works within organizational network.

        private string api_key;
        private string jwt;

        public MtoaFileService(AzureKeyVaultService azureKeyVaultService)
        {
            api_key = azureKeyVaultService.GetSecretByName("MtoaApiKey");
            jwt = azureKeyVaultService.GetSecretByName("MtoaJwt");
        }


        /// <summary> 
        ///following method uploads files after getting files from pageModel.
        // this method returns a list of attachment IDs after storing files on MTOA storage. These IDs can be used to retrieving those files at later time.
        // if there is a negative value (-1) in the List<int>, it means that the corresponding file was not uploaded successfully. Like for a virus issue.
        /// <param name="pageModel"></param>
        /// <returns> List<int> which represent the Mtoa file attachment IDs </returns>
        /// </summary>     
        public List<int> UploadFilesInPageModelAsync(UploadDocumentPageModel pageModel)
        {
            List<int> fileAttachmentIDs = null;
            // Following is a temporary static serviceRequestId.
            // TODO: serviceRequestId needs to be created at runtime through MTOA Add service request.
            int serviceRequestId = 13844; // this is used for Dev

            var fileAttachments = this.GetFileAttachmentsFromPageModel(pageModel, serviceRequestId);

            fileAttachmentIDs = new List<int>();
            foreach (var file in fileAttachments)
            {
                var storedFileAttachment = this.UploadFile(serviceRequestId, file); 
                fileAttachmentIDs.Add( storedFileAttachment.Id);
            }

            return fileAttachmentIDs;
        }


        private List<FileAttachment> GetFileAttachmentsFromPageModel(UploadDocumentPageModel pageModel, int serviceRequestId)
        {
            List<FileAttachment> attachments = null;

            if (pageModel.UploadedFiles.Count > 0)
            {
                attachments = new List<FileAttachment>();

                foreach (var file in pageModel.UploadedFiles)
                {

                    byte[] byteData=file.SelectedFileWithMemoryData.MemoryStreamData.ToArray();

                    var fileName = file.SelectedFile.Name;

                    var fileAttachment = new FileAttachment
                    {
                        ContentType = file.SelectedFile.Type,
                        Data = byteData,
                        Name = file.SelectedFile.Name,
                        ServiceRequestId = serviceRequestId,
                        Size = byteData.Length
                    };

                    attachments.Add(fileAttachment);
                }
            }

            return attachments;
        }


        public FileAttachment UploadSingleFileFromPage(UploadedFile file, int serviceRequestId)
        {
            FileAttachment uploadedFileAttachment = null;

            if (file != null)
            {
                byte[] byteData = file.SelectedFileWithMemoryData.MemoryStreamData.ToArray();

                var attachment = new FileAttachment
                {
                    ContentType = file.SelectedFile.Type,
                    Data = byteData,
                    Name = file.SelectedFile.Name,
                    ServiceRequestId = serviceRequestId,
                    Size = byteData.Length
                };

                uploadedFileAttachment = this.UploadFile(serviceRequestId, attachment).GetAwaiter().GetResult();
                file.MtoaFileAttachment = uploadedFileAttachment;
            }

            return uploadedFileAttachment;
        }

        public UploadedFile UploadSingleFileFromPageAsUpload(UploadedFile file)
        {
            UploadedFile uploadedFile = null;

            FileAttachment uploadedFileAttachment = null;
            int serviceRequestId = 13844; // this is used for Dev

            if (file != null)
            {
                byte[] byteData = file.SelectedFileWithMemoryData.MemoryStreamData.ToArray();

                var attachment = new FileAttachment
                {
                    ContentType = file.SelectedFile.Type,
                    Data = byteData,
                    Name = file.SelectedFile.Name,
                    ServiceRequestId = serviceRequestId,
                    Size = byteData.Length
                };

                uploadedFileAttachment = this.UploadFile(serviceRequestId, attachment).GetAwaiter().GetResult();
            }


            return uploadedFile;
        }


        public async Task<FileAttachment> UploadFile(int serviceRequestId, FileAttachment fileAttachment)
        {
            FileAttachment uploadedFileAttachment = null;


            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("app-jwt", this.jwt);
                client.DefaultRequestHeaders.TryAddWithoutValidation("api-key", this.api_key);

                client.BaseAddress = new Uri(this.baseURL);
                
                ByteArrayContent content = new ByteArrayContent(fileAttachment.Data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                string subUrl = "file-attachments?serviceRequestId=" + serviceRequestId +
                                "&filename=" + fileAttachment.EscapedName +
                                "&contentType=application/octet-stream&size=" + fileAttachment.Size;

                try
                {
                    System.Net.ServicePointManager.SecurityProtocol =  SecurityProtocolType.Tls12  ;
                    System.Net.ServicePointManager.Expect100Continue = false;

                    HttpResponseMessage response = client.PostAsync(subUrl, (HttpContent)content).GetAwaiter().GetResult();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        uploadedFileAttachment = await (Task<FileAttachment>)HttpContentExtensions.ReadAsAsync<FileAttachment>(response.Content);
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        uploadedFileAttachment = await (Task<FileAttachment>)HttpContentExtensions.ReadAsAsync<FileAttachment>(response.Content);
                        uploadedFileAttachment.Name = fileAttachment.Name;
                        uploadedFileAttachment.Id = -1;
                    }

                    response.EnsureSuccessStatusCode();
                    var status = response.StatusCode;
                }
                catch(WebException ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.InnerException);
                    Debug.WriteLine(ex.StackTrace);
                }
                catch (HttpRequestException socketException)
                {
                    Debug.WriteLine(socketException.Message);
                    Debug.WriteLine(socketException.InnerException);
                    Debug.WriteLine(socketException.StackTrace);
                    throw new MtoaConnectionException("Unable to connect to MTOA services.", socketException);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.InnerException);
                    Debug.WriteLine(ex.StackTrace);
                }

                return uploadedFileAttachment;
            }


        }   
    }
}

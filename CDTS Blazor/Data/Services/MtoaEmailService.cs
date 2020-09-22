using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CDNApplication.Models.PageModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MTOA.DomainObjects.DTO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Diagnostics;

namespace CDNApplication.Data.Services
{
    public class MtoaEmailService
    {
        private string api_key = "d00bfe90183b42cc8e78627c20db8d64";
        private string jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJDU0YiLCJ1bmlxdWVfbmFtZSI6IlNlYWZhcmVyIENlcnRpZmljYXRpb24gKENTRikiLCJyb2xlIjoiVXNlciIsIkNvcnJlbGF0aW9uS2V5IjoiNjI3YzIwZGI4ZDY0IiwibmJmIjoxNTk5NTk5OTUyLCJleHAiOjE2NjI2NzE5NTIsImlhdCI6MTU5OTU5OTk1MiwiaXNzIjoiVHJhbnNwb3J0IENhbmFkYSIsImF1ZCI6IlRyYW5zcG9ydCBDYW5hZGEifQ.NpFgM8AgoX0Rq0vMKVF_crLuZPxr1KJHEw-DpvlEwQc";
        private string base_uri_str = "https://wwwappstest.tc.gc.ca/Saf-Sec-Sur/13/MTAPI-INT/";
        private string sub_uri = "api/v1/notifications?overrideEmailRecipientsSafeguard=true";



        public MtoaEmailService()
        {

        }


        public EmailNotificationDTO GetEmailTemplateFromPageModel(UploadDocumentPageModel pageModel, string emailTemplateName)
        {
            EmailNotificationDTO template = new EmailNotificationDTO();
            //template.NotificationTemplateName = "SF_TEST_1_NO_PARAMS";
            //template.NotificationTemplateName = "Seafarers_Document_Submission_Email";

            template.NotificationTemplateName = emailTemplateName;
            template.ServiceRequestId = 13844;
            template.UserId = 4536;
            template.UserName = "aizimum";
            template.Language = "English";
            template.From = "mansuer.aizimu@tc.gc.ca";
            template.To = pageModel.EmailAddress;
            template.IsHtml = true;

            template.Attachements = new List<EmailAttachmentDTO>(); // in our email currently, we do not have attachments. But this field can not be null

            List<Dictionary<string, string>> parameters = new List<Dictionary<string, string>>();

            Dictionary<string, string> confirmationNumber_param = new Dictionary<string, string>();
            confirmationNumber_param.Add("Confirmation_Number", pageModel.ConfirmationNumber);
            parameters.Add(confirmationNumber_param);

            Dictionary<string, string> CDN_Number_param = new Dictionary<string, string>();
            confirmationNumber_param.Add("CDN_Number", pageModel.CdnNumber);
            parameters.Add(CDN_Number_param);

            Dictionary<string, string> Phone_Number_param = new Dictionary<string, string>();
            confirmationNumber_param.Add("Phone_Number", pageModel.PhoneNumber);
            parameters.Add(Phone_Number_param);

            Dictionary<string, string> Email_Address_param = new Dictionary<string, string>();
            confirmationNumber_param.Add("Email_Address", pageModel.EmailAddress);
            parameters.Add(Email_Address_param);

            Dictionary<string, string> Selected_CertificateType_param = new Dictionary<string, string>();
            confirmationNumber_param.Add("Selected_CertificateType", pageModel.CertificateType);
            parameters.Add(Selected_CertificateType_param);

            var document_param = GetDocumentParameters(pageModel);
            parameters.Add(document_param);


            // We assume that we have following parameters in the given template
            // Confirmation_Number 2 times.
            //CDN_Number 1
            //Phone_Number
            //Email_Address
            //Selected_CertificateType

            //DOCUMENTS
            //DOCUMENTS --needs to be generated at run time.

            //It is the list of documents in the format of
            //Document 1,2 FileName.extention
            //Type of document: Document type Entered.

            //    template.Parameters = parameters;


            return template;

        }

        private Dictionary<string, string> GetDocumentParameters(UploadDocumentPageModel pageModel)
        {
            // We assume that there is a parameter in the email template called "DOCUMENT"
            // DOCUMENT paremeter value will show up like the following:
            // Document 1: filename.doc
            // Type of document: Seatime Log

            // Document 2: myPhoto.jpg
            // Type of document: Personal Photo


            Dictionary<string, string> document_parameter = null;
            string documentValueStr = null;
            int counter = 1;

            if (pageModel.UploadedFiles.Count > 0)
            {
                document_parameter = new Dictionary<string, string>();

                foreach (var document in pageModel.UploadedFiles)
                {
                    documentValueStr = "Document " + counter.ToString() + " " + document.SelectedFile.Name + "<br/>" +
                                        "Type of document: " + document.Description + "<br/><br/>" + documentValueStr;
                    counter++;
                }
                document_parameter.Add("DOCUMENT", documentValueStr);
            }

            return document_parameter;
        }

        public async Task SendEmailToApplicant(UploadDocumentPageModel pageModel)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("app-jwt", jwt);
                client.DefaultRequestHeaders.TryAddWithoutValidation("api-key", api_key);

                client.BaseAddress = new Uri(base_uri_str);

                string templateName = "TemplateName";
                var emailTemplate = GetEmailTemplateFromPageModel(pageModel , templateName);
                HttpResponseMessage response = null;

                try
                {
                    response = await client.PostAsJsonAsync<EmailNotificationDTO>(sub_uri, emailTemplate);
                    response.EnsureSuccessStatusCode();
                    var status = response.StatusCode;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                }
            }
        }




    }
}



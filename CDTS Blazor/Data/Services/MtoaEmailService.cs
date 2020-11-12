namespace CDNApplication.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CDNApplication.Models.PageModels;
    using System.Net.Http;
    using System.Diagnostics;
    using CDNApplication.Data.DTO.MTAPI;

    public class MtoaEmailService
    {
        private string api_key;
        private string jwt;
        private string base_uri_str = "https://wwwappstest.tc.gc.ca/Saf-Sec-Sur/13/MTAPI-INT/";
        private string sub_uri = "api/v1/notifications?overrideEmailRecipientsSafeguard=true";

        public MtoaEmailService(AzureKeyVaultService azureKeyVaultService)
        {

            api_key = azureKeyVaultService.GetSecretByName("MtoaApiKey");

            jwt = azureKeyVaultService.GetSecretByName("MtoaJwt");
        }


        public EmailNotificationDTO GetEmailTemplateFromPageModel(UploadDocumentPageModel pageModel)
        {
            // this method uses the email template: "Seafarers_Document_Submission_Email"

            EmailNotificationDTO template = new EmailNotificationDTO();
            template.NotificationTemplateName = "Seafarers_Document_Submission_Email";
            template.ServiceRequestId = 13844;
            template.UserId = 4536;
            template.UserName = "Nobody";
            template.Language = "English";
            template.From = "donotreply-nepasrepondre@tc.gc.ca";
            template.To = pageModel.EmailAddress;
            template.IsHtml = true;

            template.Attachements = new List<EmailAttachmentDTO>(); // in our email currently, we do not have attachments. But this field can not be null

            var parameters = new List<KeyValuePair<string, string>>();


            parameters.Add(new KeyValuePair<string, string>("Confirmation_Number", pageModel.ConfirmationNumber));
            parameters.Add(new KeyValuePair<string, string>("CDN_Number", pageModel.CdnNumber));
            parameters.Add(new KeyValuePair<string, string>("Phone_Number", pageModel.PhoneNumber));
            parameters.Add(new KeyValuePair<string, string>("Email_Address", pageModel.EmailAddress));
            parameters.Add(new KeyValuePair<string, string>("Selected_CertificateType", pageModel.CertificateType));
            parameters.Add(new KeyValuePair<string, string>("Confirmation_Number", pageModel.ConfirmationNumber));

            var document_param = GetDocumentParameters(pageModel);
            parameters.Add(document_param);

            template.Parameters = parameters;

            #region-- email template parameters
            // We assume that we have following parameters in the given template
            // Confirmation_Number
            //CDN_Number
            //Phone_Number
            //Email_Address
            //Selected_CertificateType

            //DOCUMENT
            //DOCUMENT --this parameter consists of many document fields combined in one string.

            //It is the list of documents in the format of
            //Document 1 FileName.extention
            //Type of document: Document type Entered.
            #endregion--- end of template parameter information

            return template;
        }

        private KeyValuePair<string, string> GetDocumentParameters(UploadDocumentPageModel pageModel)
        {
            // We assume that there is a parameter in the email template called "DOCUMENT"
            // DOCUMENT paremeter value will show up like the following:
            // Document 1: filename.doc
            // Type of document: Seatime Log

            // Document 2: myPhoto.jpg
            // Type of document: Personal Photo


            KeyValuePair<string, string> document_parameter = new KeyValuePair<string, string>();
            string documentValueStr = null;
            int counter = 1;

            if (pageModel.UploadedFiles.Count > 0)
            {
                document_parameter = new KeyValuePair<string, string>();

                foreach (var document in pageModel.UploadedFiles)
                {
                    documentValueStr = documentValueStr  +
                                       "Document " + counter.ToString() + " " + document.SelectedFile.Name + "<br/>" +
                                       "Type of document: " + document.Description + "<br><br>";
                    counter++;
                }
                document_parameter= new KeyValuePair<string, string>("DOCUMENT", documentValueStr);
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

                var emailTemplate = GetEmailTemplateFromPageModel(pageModel );
                HttpResponseMessage response = null;

                try
                {
                    response = await client.PostAsJsonAsync<EmailNotificationDTO>(sub_uri, emailTemplate).ConfigureAwait(false);
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



namespace CSF.Web.Client.Models.PageModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using CSF.Web.Client.Data;

    /// <summary>
    /// PageModel for the UploadDocument page.
    /// </summary>
    public class UploadDocumentPageModel
    {
        /// <summary>
        /// Gets or sets CDN number.
        /// </summary>
        public string CdnNumber { get; set; }

        /// <summary>
        /// Gets or sets Email Address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets Confirmation Number.
        /// </summary>
        public string ConfirmationNumber { get; set; }

        /// <summary>
        /// Gets or sets the certificate type.
        /// </summary>
        public string CertificateType { get; set; }

        /// <summary>
        /// Gets the list of uploaded files.
        /// </summary>
        public List<UploadedFile> UploadedFiles { get; } = new List<UploadedFile>();

        /// <summary>
        /// Gets or sets the file description.
        /// </summary>
        public string FileDescription { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the service request id from Mtoa.
        /// </summary>
        public int MtoaServiceRequestId { get; set; }
        /// <summary>
        /// Gets or sets Renewing.
        /// </summary>
        public SubmissionType SubmissionType { get; set; }

        /// <summary>
        /// Gets or sets NotRobot value which is associated with recaptcha element.
        /// </summary>
        [Required(ErrorMessage ="I am not a robot field must be checked")]
        public string NotRobot { get; set; }

        /// <summary>
        /// Gets the uploaded files in a format that can be used in mtoa.
        /// </summary>
        public string ToMtoaDocumentString
        {
            get
            {
                var mtoaDocumentStringBuilder = new StringBuilder();
                for (int i = 0; i < this.UploadedFiles.Count; i++)
                {
                    var uploadedFile = this.UploadedFiles[i];
                    var documentRunningCount = i + 1;
                    mtoaDocumentStringBuilder.Append(string.Format("Document {0} {1}<br />Type of document: {2}<br /><br />", documentRunningCount, uploadedFile.SelectedFile.Name, uploadedFile.Description));
                }

                return mtoaDocumentStringBuilder.ToString();
            }
        }

        /// <summary>
        /// Override of Object.ToString() method.
        /// </summary>
        /// <returns>List of all uploaded files.</returns>
        public override string ToString()
        {
            return string.Format(
                "{1}{0} {2}{0} {3}{0}",
                Environment.NewLine,
                this.CdnNumber,
                this.CertificateType,
                this.UploadedFiles);
        }
    }
}
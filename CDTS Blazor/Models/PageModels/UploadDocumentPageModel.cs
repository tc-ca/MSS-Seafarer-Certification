namespace CDNApplication.Models.PageModels
{
    using System;
    using System.Collections.Generic;
    using CDNApplication.Data.Entity;

    /// <summary>
    ///     PageModel for the UploadDocument page.
    /// </summary>
    public class UploadDocumentPageModel
    {
        /// <summary>
        ///     Gets or sets CDN number.
        /// </summary>
        public string CdnNumber { get; set; }

        /// <summary>
        ///     Gets or sets Email Address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        ///     Gets or sets PhoneNumber.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Gets or sets Confirmation Number.
        /// </summary>
        public string ConfirmationNumber { get; set; }
        /// <summary>
        ///     Gets or sets the certificate type.
        /// </summary>
        public string CertificateType { get; set; }

        /// <summary>
        ///     Gets the list of uploaded files.
        /// </summary>
        public List<UploadedFile> UploadedFiles { get; } = new List<UploadedFile>();

        /// <summary>
        ///     Gets or sets the file description.
        /// </summary>
        public string FileDescription { get; set; } = string.Empty;

        /// <summary>
        ///     Override of Object.ToString() method.
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
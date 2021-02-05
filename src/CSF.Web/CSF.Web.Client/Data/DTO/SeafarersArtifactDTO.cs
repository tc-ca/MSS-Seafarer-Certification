using CSF.Web.Client.Data.DTO.MTAPI;
using System;
using System.Collections.Generic;

namespace CSF.Web.Client.Data.DTO
{
    /// <summary>
    /// Model to send to MTAO.
    /// </summary>
    public class SeafarersArtifactDTO
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
        /// Gets or sets the list of uploaded files.
        /// </summary>
        public List<MtoaFileInfo> UploadedFilesInfo { get; set; }

        /// <summary>
        /// Gets or sets Renewing.
        /// </summary>
        public string SubmissionType { get; set; }

        /// <summary>
        /// Gets or sets submission.
        /// </summary>
        public string SubmissionProgress { get; set; }
    }
}

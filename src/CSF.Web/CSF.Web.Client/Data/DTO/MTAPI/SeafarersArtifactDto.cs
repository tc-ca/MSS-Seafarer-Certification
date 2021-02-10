namespace CSF.Web.Client.Data.DTO.MTAPI
{
    using System;
    using System.Collections.Generic;
    using CSF.Web.Client.Data.DTO.MTAPI;
    using CSF.Web.Client.Models.PageModels;

    /// <summary>
    /// Model to send to MTAO.
    /// </summary>
    public class SeafarersArtifactDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeafarersArtifactDto"/> class.
        /// </summary>
        public SeafarersArtifactDto()
        {
            this.UploadedFilesInfo = new List<MtoaFileInfo>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeafarersArtifactDto"/> class.
        /// </summary>
        /// <param name="uploadDoucmentPageModel">The page model to transfer from.</param>
        public SeafarersArtifactDto(UploadDocumentPageModel uploadDoucmentPageModel)
        {
            if (uploadDoucmentPageModel == null)
            {
                throw new ArgumentNullException(nameof(uploadDoucmentPageModel));
            }

            this.CdnNumber = uploadDoucmentPageModel.CdnNumber;
            this.CertificateType = uploadDoucmentPageModel.CertificateType;
            this.ConfirmationNumber = uploadDoucmentPageModel.ConfirmationNumber;
            this.EmailAddress = uploadDoucmentPageModel.EmailAddress;
            this.PhoneNumber = uploadDoucmentPageModel.PhoneNumber;
            this.SubmissionType = uploadDoucmentPageModel.SubmissionType.ToString();
            this.UploadedFilesInfo = new List<MtoaFileInfo>();

            for (var i = 0; i < uploadDoucmentPageModel.UploadedFiles.Count; i++)
            {
                this.UploadedFilesInfo.Add(new MtoaFileInfo()
                {
                    FileId = i, // Temporary solution for now.
                    FileDescription = uploadDoucmentPageModel.UploadedFiles[i].Description,
                });
            }
        }

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

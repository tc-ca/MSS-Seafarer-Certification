﻿namespace CDNApplication.Data.DTO.MTAPI
{
    using Data.Attributes;

    /// <summary>
    /// The parameters for the MTOA seafarer document submission email.
    /// </summary>
    public class MtoaSeafarersDocumentSubmissionEmailParametersDto
    {
        /// <summary>
        /// Gets or sets the seafarer's confirmation number.
        /// </summary>
        [MtoaNotificationParameterName("Confirmation_Number")]
        public string ConfirmationNumber { get; set; }

        /// <summary>
        /// Gets or sets the seafarer's Candidate document number (CDN) number.
        /// </summary>
        [MtoaNotificationParameterName("CDN_Number")]
        public string CdnNumber { get; set; }

        /// <summary>
        /// Gets or sets the seafarer's phone number.
        /// </summary>
        [MtoaNotificationParameterName("Phone_Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the seafarer's email address.
        /// </summary>
        [MtoaNotificationParameterName("Email_Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the seafarer's selected certificate.
        /// </summary>
        [MtoaNotificationParameterName("Selected_CertificateType")]
        public string CertificateType { get; set; }

        /// <summary>
        /// Gets or sets the submission type english string.
        /// </summary>
        [MtoaNotificationParameterName("Submission_Type_En")]
        public string SubmissionTypeEnglish { get; set; }

        /// <summary>
        /// Gets or sets the submission type french string.
        /// </summary>
        [MtoaNotificationParameterName("Submission_Type_Fr")]
        public string SubmissionTypeFrench { get; set; }
    }
}

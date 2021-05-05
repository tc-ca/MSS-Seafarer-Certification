namespace MPDIS.API.Wrapper.Services.MPDIS.Entities
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Defines the full applicant information.
    /// </summary>
    public class FullApplicantInformation : TrimmedApplicantInformation
    {
        /// <summary>
        /// Gets or sets the applicant's cdn issued date in unix epoch time.
        /// </summary>
        [JsonProperty("candidateNumIssuedDate")]
        public long CdnIssuedDate { get; set; }

        /// <summary>
        /// Gets or sets the applicant's discharged book issue date in unix epoch time.
        /// </summary>
        [JsonProperty("dischargedBookIssuedDate")]
        public long? DischargedBookIssueDate { get; set; }

        /// <summary>
        /// Gets or sets the applicant's s card identifier issue date in unix epoch time.
        /// </summary>
        [JsonProperty("sIDCardIssuedDate")]
        public long? SIdCardIssueDate { get; set; }

        /// <summary>
        /// Gets or sets the applicant's remarks.
        /// </summary>
        [JsonProperty("remarks")]
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the applicant's identity document number.
        /// </summary>
        [JsonProperty("identityDocNbr")]
        public string IdentityDocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets the applicant's identity document type.
        /// </summary>
        [JsonProperty("identityDocType")]
        public string IdentityDocumentType { get; set; }

        /// <summary>
        /// Gets or sets the applicant's identity document identifier.
        /// </summary>
        [JsonProperty("identityDocId")]
        public int? IdentityDocumentId { get; set; }

        /// <summary>
        /// Gets or sets the applicant's identity document rmarks.
        /// </summary>
        [JsonProperty("identityRemarks")]
        public string IdentityRemarks { get; set; }

        /// <summary>
        /// Gets or sets the applicant's citizenship document number.
        /// </summary>
        [JsonProperty("citizenshipDocNbr")]
        public string CitizenshipDocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets the applicant's citizenship document type.
        /// </summary>
        [JsonProperty("citizenshipDocType")]
        public string CitizenshipDocumentType { get; set; }

        /// <summary>
        /// Gets or sets the applicant's citizenship document identifier.
        /// </summary>
        [JsonProperty("citizenshipDocId")]
        public int? CitizenshipDocumentId { get; set; }

        /// <summary>
        /// Gets or sets the applicant's citizenship country.
        /// </summary>
        [JsonProperty("citizenshipCountry")]
        public string CitizenshipCountry { get; set; }

        /// <summary>
        /// Gets or sets the applicant's citizenship notes.
        /// </summary>
        [JsonProperty("citizenshipNotes")]
        public string CitizenshipNotes { get; set; }

        /// <summary>
        /// Gets or sets the applicant's physician assesment.
        /// </summary>
        [JsonProperty("physicianAssessment")]
        public string PhysicianAssesment { get; set; }

        /// <summary>
        /// Gets or sets the applicant's physician exam date in unix epoch time.
        /// </summary>
        [JsonProperty("examDate")]
        public long? PhysicianExamDate { get; set; }

        /// <summary>
        /// Gets or sets the applicant's physician exam expiration date in unix epoch time.
        /// </summary>
        [JsonProperty("expireDate")]
        public long? PhysicianExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the applicant's physician assesment.
        /// </summary>
        [JsonProperty("assessment")]
        public string Assesment { get; set; }

        /// <summary>
        /// Gets or sets the applicant's minister certification issue date in unix epoch time.
        /// </summary>
        [JsonProperty("ministerCertIssuedDate")]
        public long? MinisiterCertificationIssueDate { get; set; }

        /// <summary>
        /// Gets or sets the applicant's minister certification expiration date in unix epoch time.
        /// </summary>
        [JsonProperty("ministerCertExpireDate")]
        public long? MinisiterCertificationExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the applicant's medical notes.
        /// </summary>
        [JsonProperty("medicalNotes")]
        public string MedicalNotes { get; set; }

        /// <summary>
        /// Gets or sets the applicant's photo.
        /// </summary>
        [JsonProperty("photo")]
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicant's citizenship has been checked.
        /// </summary>
        [JsonProperty("citizenshipChecked")]
        public bool? CitizenshipChecked { get; set; }
    }
}
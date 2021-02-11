namespace MPDIS.API.Wrapper.Services.MPDIS.Entities
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the applicant information.
    /// </summary>
    public class ApplicantInformation
    {
        /// <summary>
        /// Gets or sets a value indicating whether whether the data comes from ACES.
        /// </summary>
        [JsonProperty("fromACES")]
        public bool FromAces { get; set; }

        /// <summary>
        /// Gets or sets the applicant's unique identifier.
        /// </summary>
        [JsonProperty("id")]

        public int Id
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the applicant's first name.
        /// </summary>
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the applicant's last name.
        /// </summary>
        [JsonProperty("lastname")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the applicant's cdn number.
        /// </summary>
        [JsonProperty("cdn")]
        public string Cdn { get; set; }

        /// <summary>
        /// Gets or sets the applicant's decease status.
        /// </summary>
        [JsonProperty("deceasedStatus")]

        public bool? DeceasedStatus { get; set; }

        /// <summary>
        /// Gets or sets the applicant's date of birth in unix epoch time.
        /// </summary>
        [JsonProperty("dateOfBirth")]
        public long DateOfBith { get; set; }

        /// <summary>
        /// Gets or sets the applicant's address identifier.
        /// </summary>
        [JsonProperty("addressId")]
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets the applicant's address.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the applicant's city.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the applicant's province.
        /// </summary>
        [JsonProperty("province")]
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the applicant's postal code.
        /// </summary>
        [JsonProperty("postCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the applicant's home country.
        /// </summary>
        [JsonProperty("homeCountry")]
        public string HomeCountry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicant's mail address is the same or not.
        /// </summary>
        [JsonProperty("sameMailAddress")]
        public bool SameMailAddress { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing address identifier.
        /// </summary>
        [JsonProperty("mailingAddressId")]
        public int MailingAddressId { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing address.
        /// </summary>
        [JsonProperty("mailAddress")]
        public string MailAddress { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing city.
        /// </summary>
        [JsonProperty("mailCity")]
        public string MailCity { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing province.
        /// </summary>
        [JsonProperty("mailProvince")]
        public string MailProvince { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing postal code.
        /// </summary>
        [JsonProperty("mailPostCode")]
        public string MailPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing home country.
        /// </summary>
        [JsonProperty("mailHomeCountry")]
        public string MailHomeCountry { get; set; }

        /// <summary>
        /// Gets or sets the applicant's contact identifier.
        /// </summary>
        [JsonProperty("contactId")]
        public int ContactId { get; set; }

        /// <summary>
        /// Gets or sets the applicant's phone number.
        /// </summary>
        [JsonProperty("phoneNum")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the applicant's second phone number.
        /// </summary>
        [JsonProperty("secondaryPhoneNum")]
        public string SecondaryPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the applicant's email.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the applicant's gender.
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the applicant's selected language.
        /// </summary>
        [JsonProperty("selectedLang")]
        public string SelectedLanguage { get; set; }

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
        public int IdentityDocumentId { get; set; }

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
        public int CitizenshipDocumentId { get; set; }

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
        public long PhysicianExamDate { get; set; }

        /// <summary>
        /// Gets or sets the applicant's physician exam expiration date in unix epoch time.
        /// </summary>
        [JsonProperty("expireDate")]
        public long PhysicianExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the applicant's physician assesment.
        /// </summary>
        [JsonProperty("assessment")]
        public string Assesment { get; set; }

        /// <summary>
        /// Gets or sets the applicant's minister certification issue date in unix epoch time.
        /// </summary>
        [JsonProperty("ministerCertIssuedDate")]
        public long MinisiterCertificationIssueDate { get; set; }

        /// <summary>
        /// Gets or sets the applicant's minister certification expiration date in unix epoch time.
        /// </summary>
        [JsonProperty("ministerCertExpireDate")]
        public long MinisiterCertificationExpirationDate { get; set; }

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

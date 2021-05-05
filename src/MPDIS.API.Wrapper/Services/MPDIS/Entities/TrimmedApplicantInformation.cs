using Newtonsoft.Json;
using System;

namespace MPDIS.API.Wrapper.Services.MPDIS.Entities
{
    /// <summary>
    /// Variation of the <see cref="FullApplicantInformation"/> without Protected B data or anything else we do not need for our client application.
    /// </summary>
    public class TrimmedApplicantInformation
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
        /// Gets or sets the applicant's date of birth in unix epoch time.
        /// </summary>
        [JsonProperty("dateOfBirth")]
        public long DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the applicant's address identifier.
        /// </summary>
        [JsonProperty("addressid")]
        public int? HomeAddressId { get; set; }

        /// <summary>
        /// Gets or sets the applicant's address.
        /// </summary>
        [JsonProperty("address")]
        public string HomeAddress { get; set; }

        /// <summary>
        /// Gets or sets the applicant's city.
        /// </summary>
        [JsonProperty("city")]
        public string HomeAddressCity { get; set; }

        /// <summary>
        /// Gets or sets the applicant's province.
        /// </summary>
        [JsonProperty("province")]
        public string HomeAddressProvince { get; set; }

        /// <summary>
        /// Gets or sets the applicant's postal code.
        /// </summary>
        [JsonProperty("postCode")]
        public string HomeAddressPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the applicant's home country.
        /// </summary>
        [JsonProperty("homeCountry")]
        public string HomeAddressCountry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicant's mail address is the same or not.
        /// </summary>
        [JsonProperty("sameMailAddress")]
        public bool SameMailAddress { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing address identifier.
        /// </summary>
        [JsonProperty("mailingAddressId")]
        public int? MailingAddressId { get; set; }

        /// <summary>
        /// Gets or sets the applicant's contact identifier.
        /// </summary>
        [JsonProperty("contactId")]
        public int ContactId { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing address.
        /// </summary>
        [JsonProperty("mailAddress")]
        public string MailingAddress { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing city.
        /// </summary>
        [JsonProperty("mailCity")]
        public string MailingAddressCity { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing province.
        /// </summary>
        [JsonProperty("mailProvince")]
        public string MailingAddressProvince { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing postal code.
        /// </summary>
        [JsonProperty("mailPostCode")]
        public string MailingAddressPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the applicant's mailing home country.
        /// </summary>
        [JsonProperty("mailHomeCountry")]
        public string MailingAddressCountry { get; set; }

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

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
        public string FullGender
        {
            get
            {
                if (string.Equals("M", this.Gender))
                {
                    return "Male";
                }
                else
                {
                    return "Female";
                }
            }
        }

        public string DateOfBirthString
        {
            get
            {
                DateTimeOffset offset = DateTimeOffset.FromUnixTimeMilliseconds(this.DateOfBirth);
                var DOB = offset.DateTime;
                return DOB.ToString("MMMM dd, yyyy");
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPDIS.API.Wrapper.Services.MPDIS.Entities
{
    public class PersonalInfo
    {
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("cdn")]
        public string Cdn { get; set; }
        [JsonProperty("dateOfBirth")]
        public long DateOfBirth { get; set; }

        [JsonProperty("address")]
        public string HomeAddress { get; set; }

        [JsonProperty("city")]
        public string HomeAddressCity { get; set; }

        [JsonProperty("province")]
        public string HomeAddressProvince { get; set; }

        [JsonProperty("postCode")]
        public string HomeAddressPostalCode { get; set; }

        [JsonProperty("homeCountry")]
        public string HomeAddressCountry { get; set; }


        [JsonProperty("mailAddress")]
        public string MailingAddress { get; set; }

        [JsonProperty("mailCity")]
        public string MailingAddressCity { get; set; }

        [JsonProperty("mailProvince")]
        public string MailingAddressProvince { get; set; }

        [JsonProperty("mailPostCode")]
        public string MailingAddressPostalCode { get; set; }

        [JsonProperty("mailHomeCountry")]
        public string MailingAddressCountry { get; set; }

        [JsonProperty("phoneNum")]
        public string PhoneNumber { get; set; }

        [JsonProperty("secondaryPhoneNum")]
        public string SecondaryPhoneNumber { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("selectedLang")]
        public string Language { get; set; }
    }
}

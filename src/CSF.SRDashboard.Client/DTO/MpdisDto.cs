using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO
{
    public class MpdisDto
    {
        public bool FromAces { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cdn { get; set; }
        public long DateOfBirth { get; set; }
        public int? HomeAddressId { get; set; }
        public string HomeAddress { get; set; }
        public string HomeAddressCity { get; set; }
        public string HomeAddressProvince { get; set; }
        public string HomeAddressPostalCode { get; set; }
        public string HomeAddressCountry { get; set; }
        public bool SameMailAddress { get; set; }
        public int? MailingAddressId { get; set; }
        public int ContactId { get; set; }
        public string MailingAddress { get; set; }
        public string MailingAddressCity { get; set; }
        public string MailingAddressProvince { get; set; }
        public string MailingAddressPostalCode { get; set; }
        public string MailingAddressCountry { get; set; }
        public string PhoneNumber { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string SelectedLanguage { get; set; }
        public string FullName { get; set; }
        public string DateOfBirthString { get; set; }
    }

}

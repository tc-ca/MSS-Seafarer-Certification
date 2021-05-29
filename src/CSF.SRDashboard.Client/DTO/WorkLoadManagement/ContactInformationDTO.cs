using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.WorkLoadManagement
{
    public class ContactInformationDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string CountryIso3 { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string PrefferedContactMethod { get; set; }

        public bool PrimaryContactInd { get; set; }

        public string ContactUniqueId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDateUTC { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDateUTC { get; set; }

        public bool? DeletedInd { get; set; }

        public string DeletedBy { get; set; }

        public DateTime? DeletedDateUTC { get; set; }
    }
}

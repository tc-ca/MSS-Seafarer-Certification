using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO
{
    public class BaseServiceRequest
    {
        public BaseServiceRequest()
        {
        }

        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Guid { get; set; }
        [JsonProperty]
        public int UserId { get; set; }
        [JsonProperty]
        public int ServiceId { get; set; }
        [JsonProperty]
        public int? OrganizationId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty]
        public ServiceRequestStatus RequestStatus { get; set; }
        [JsonProperty]
        public string EnglishDisplayName { get; set; }
        [JsonProperty]
        public string FrenchDisplayName { get; set; }
        [JsonProperty]
        public int? MetadataStructureId { get; set; }
        [JsonProperty]
        public int? ServiceAttributeId { get; set; }
        [JsonProperty]
        public string Comment { get; set; }
        [JsonProperty]
        public int? RequesterId { get; set; }
        [JsonProperty]
        public int? MailCoordinateId { get; set; }
    }
}

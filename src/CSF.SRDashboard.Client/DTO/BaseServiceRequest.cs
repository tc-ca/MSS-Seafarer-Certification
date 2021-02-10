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

        [JsonProperty(Order = -17)]
        public int Id { get; set; }
        [JsonProperty(Order = -16)]
        public string Guid { get; set; }
        [JsonProperty(Order = -15)]
        public int UserId { get; set; }
        [JsonProperty(Order = -14)]
        public int ServiceId { get; set; }
        [JsonProperty(Order = -13)]
        public int? OrganizationId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(Order = -12)]
        public ServiceRequestStatus RequestStatus { get; set; }
        [JsonProperty(Order = -11)]
        public string EnglishDisplayName { get; set; }
        [JsonProperty(Order = -10)]
        public string FrenchDisplayName { get; set; }
        [JsonProperty(Order = -9)]
        public int? MetadataStructureId { get; set; }
        [JsonProperty(Order = -8)]
        public int? ServiceAttributeId { get; set; }
        [JsonProperty(Order = -7)]
        public string Comment { get; set; }
        [JsonProperty(Order = -6)]
        public int? RequesterId { get; set; }
        [JsonProperty(Order = -5)]
        public int? MailCoordinateId { get; set; }
    }
}

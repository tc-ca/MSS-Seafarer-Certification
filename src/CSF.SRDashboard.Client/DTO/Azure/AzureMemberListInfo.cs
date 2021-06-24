using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.Azure
{
    public class AzureMemberListInfo
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }
        public List<AzureMemberInfo> value { get; set; }
    }
}

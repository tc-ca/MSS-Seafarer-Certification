using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.Azure
{
    public class AzureMemberInfo
    {
        public List<object> businessPhones { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
        public string jobTitle { get; set; }
        public string mail { get; set; }
        public object mobilePhone { get; set; }
        public object officeLocation { get; set; }
        public object preferredLanguage { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
        public string id { get; set; }

        string _names;
        public string Names
        {
            get 
            { 
                if(_names != null)
                {
                    return _names;
                }
                else
                {
                    return ((surname != null) ? surname + "," + givenName : null);
                }
            }

            set 
            {
                _names = value;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    
    public class FindSeafarerSearchCriteria
    {
        public int Page { get; set; } = 0;

        public int PageSize { get; set; } = 20;

        public string CDNNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DateOfBirth { get; set; }

    }
}

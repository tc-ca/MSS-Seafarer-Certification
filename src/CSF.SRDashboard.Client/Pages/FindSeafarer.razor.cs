using Microsoft.AspNetCore.Components;
using MPDIS.API.Wrapper.Services.MPDIS;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class FindSeafarer
    {
        [Inject]
        public IMpdisService MpdisService { get; set; }

        public string json { get; set; }

        private seafarerForm sea = new seafarerForm();

        public class seafarerForm
        {
            public string CDNValue { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public DateTime DOB { get; set; }

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            this.Search();
        }

        public void Search()
        {

            var dummy = new ApplicantSearchCriteria
            {
                Cdn = "00000176",
                DateOfBirth = string.Empty,
                FirstName = string.Empty,
                LastName = string.Empty
            };

            var result =  this.MpdisService.Search(dummy);

            json = JsonConvert.SerializeObject(result.Items, Formatting.Indented);

        }

    }
}

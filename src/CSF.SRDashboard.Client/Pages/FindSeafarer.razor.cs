using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
        protected EditContext editContext;

        [Inject]
        public IMpdisService MpdisService { get; set; }

        public string json { get; set; }

        public ApplicantSearchCriteria searchCriteria = new ApplicantSearchCriteria();
       // protected seafarerForm sea { get; set; }

        /*public class seafarerForm
        {
            public string CDNValue { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public DateTime? DOB { get; set; }

        }*/

        protected override void OnInitialized()
        {
            base.OnInitialized();
           
            editContext = new EditContext(searchCriteria);
            var isValid = editContext.Validate();

            //this.Search();
        }

        public void Search()
        {
           
           

            var result =  this.MpdisService.Search(searchCriteria);

            json = JsonConvert.SerializeObject(result.Items, Formatting.Indented);

        }

    }
}

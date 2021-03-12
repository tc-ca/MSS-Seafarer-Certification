namespace CSF.SRDashboard.Client.Pages
{
    using System;
    using System.Collections.Generic;
    using CSF.SRDashboard.Client.Models;
    using CSF.SRDashboard.Client.Services;
    using CSF.SRDashboard.Client.Utilities;
    using Microsoft.AspNetCore.Components;

    public partial class ServiceDetails
    {
        [Inject]
        public IMtoaArtifactService MtoaArtifactService { get; set; }

        public RequestDetailComponentModel RequestDetailComponentModel { get; set; }

        public MMEDetailComponentModel MMEDetailComponentModel { get; set; }
        
        /// <summary>
        /// Gets or sets the service request identification number.
        /// </summary>
        [Parameter]
        public int ServiceRequestId { get; set; }

        public RequestDetailsPageModel RequestDetailsPageData { get; set; }

        public List<Note> NotesData { get; set; }

        public ServiceDetails()
        {
            this.NotesData = new List<Note>();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                var model = this.MtoaArtifactService.GetArtifactByServiceRequestId(this.ServiceRequestId);
                // Random values
                var rng = new Random();
                DateTime RandomDay()
                {
                    DateTime start = new DateTime(2021, 1, 1);
                    int range = (DateTime.Today - start).Days;
                    return start.AddDays(rng.Next(range));
                }

                this.RequestDetailComponentModel = new RequestDetailComponentModel
                {
                    CertificateType = model.CertificateType,
                    RequestType = "Marine medical certificate Dummy",
                    TriageType = "Fast Track Dummy"
                };

                this.MMEDetailComponentModel = new MMEDetailComponentModel
                {
                    Name = "Dr. Wendy Smith",
                    MmeNumber = rng.Next(1000, 100000),
                    ExpiryDate = RandomDay()
                };
                
                // Notes mock
                var Notes = new List<Note>
                {
                    new Note
                    {
                        Id = 0,
                        DateCreated = DateTime.Today,
                        FirstName = "Chris",
                        LastName = "Ikongo",
                        Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod"
                    },
                    new Note
                    {
                        Id = 1,
                        DateCreated = RandomDay(),
                        FirstName = "Lionel",
                        LastName = "Messi",
                        Text = "Alerts are available for any length of text, as well as an optional dismiss button."
                    },
                    new Note
                    {
                        Id = 2,
                        DateCreated = RandomDay(),
                        FirstName = "John",
                        LastName = "Wick",
                        Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod 563323"
                    },
                    new Note
                    {
                        Id = 3,
                        DateCreated = new DateTime(2021,1,5),
                        FirstName = "Bill",
                        LastName = "Notes",
                        Text = "Before getting started with Bootstrap’s modal component, be sure to read the following as our menu options have recently changed."
                    }

                };

                //NotesData = Notes;
                InvokeAsync(StateHasChanged);
            }
            base.OnAfterRender(firstRender);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            var utility = new Utility(MtoaArtifactService);

            RequestDetailsPageData = utility.CreateMockRequestDetailsData();
        }
    }
}
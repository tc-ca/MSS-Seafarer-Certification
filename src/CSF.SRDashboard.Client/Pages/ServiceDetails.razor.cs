namespace CSF.SRDashboard.Client.Pages
{
    using CSF.SRDashboard.Client.DTO;
    using CSF.SRDashboard.Client.Models;
    using CSF.SRDashboard.Client.Services;
    using CSF.SRDashboard.Client.Utilities;
    using Microsoft.AspNetCore.Components;
    using Radzen.Blazor;
    using System;
    using System.Collections.Generic;

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

        public List<NoteDTO> Notes { get; set; }

        protected RadzenGrid<DashboardRow> NotesGrid;

        protected List<DashboardRow> RequestsInNotes;

        protected int NumberOfNotes;

        protected int _NotesPageSize;

        public List<DashboardRow> NotesData { get; set; }

        DashboardRow oneRow;

        protected int NotesPageSize
        {
            get
            {
                return _NotesPageSize;
            }
            set
            {
                _NotesPageSize = value;

                StateHasChanged();

                if (NotesGrid != null)
                {
                    NotesGrid.Reload();
                }
            }
        }

        void OnRowDoubleClick(DashboardRow row)
        {
            //var requestNumber = row.ServiceRequestNumber;

            //navigationManger.NavigateTo($"{navigationManger.BaseUri}/requestdetails/" + row.ServiceRequestNumber);
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

                Notes = new List<NoteDTO>
                {
                    new NoteDTO
                    {
                        DateCreated = DateTime.Today,
                        FirstName = "Chris",
                        LastName = "Ikongo",
                        Note = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod"
                    },
                    new NoteDTO
                    {
                        DateCreated = RandomDay(),
                        FirstName = "Lionel",
                        LastName = "Messi",
                        Note = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod 563323"
                    },
                    new NoteDTO
                    {
                        DateCreated = RandomDay(),
                        FirstName = "John",
                        LastName = "Wick",
                        Note = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod 563323"
                    }

                };

                InvokeAsync(StateHasChanged);
            }
            base.OnAfterRender(firstRender);
        }

        protected override void OnInitialized()
        {
            var utility = new Utility(MtoaArtifactService);

            RequestDetailsPageData = utility.CreateMockRequestDetailsData();

            base.OnInitialized();
        }


    }
}
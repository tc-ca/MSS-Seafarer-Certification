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
    using System.Linq;

    public partial class ServiceDetails
    {
        public string Author { get; set; }

        public string NoteText { get; set; }

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

        protected RadzenGrid<NoteDTO> NotesGrid;

        protected List<NoteDTO> RequestsInNotes;

        protected int NumberOfNotes;

        protected int _NotesPageSize;

        public List<NoteDTO> NotesData { get; set; }

        NoteDTO OneRow;

        // Number of items on the grid
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

        void OnRowDoubleClick(NoteDTO row)
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
                var Notes = new List<NoteDTO>
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

                NotesData = Notes;

                NotesPageSize = Notes.Count;

                OneRow = Notes.FirstOrDefault();

                InvokeAsync(StateHasChanged);
            }
            base.OnAfterRender(firstRender);
        }

        protected override void OnInitialized()
        {
            var utility = new Utility(MtoaArtifactService);

            RequestDetailsPageData = utility.CreateMockRequestDetailsData();

            NotesGrid = new RadzenGrid<NoteDTO>();

            base.OnInitialized();
        }

        protected void CreateNote()
        {
            var note = new NoteDTO
            {
                DateCreated = DateTime.Now,
                FirstName = "Alex",
                LastName = this.Author,
                Note = string.Format("Note Text {0} - {1}", this.NoteText, this.NotesData.Count)
            };

            NotesData.Add(note);
            this.NotesGrid.Reload();
        }

    }
}
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
        public int NoteId { get; set; } = -1;

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
                        Id = 0,
                        DateCreated = DateTime.Today,
                        FirstName = "Chris",
                        LastName = "Ikongo",
                        Note = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod"
                    },
                    new NoteDTO
                    {
                        Id = 1,
                        DateCreated = RandomDay(),
                        FirstName = "Lionel",
                        LastName = "Messi",
                        Note = "Alerts are available for any length of text, as well as an optional dismiss button."
                    },
                    new NoteDTO
                    {
                        Id = 2,
                        DateCreated = RandomDay(),
                        FirstName = "John",
                        LastName = "Wick",
                        Note = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod 563323"
                    },
                    new NoteDTO
                    {
                        DateCreated = new DateTime(2021,1,5),
                        FirstName = "Bill",
                        LastName = "Notes",
                        Note = "Before getting started with Bootstrap’s modal component, be sure to read the following as our menu options have recently changed."
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
            if (this.isNewNote())
            {
                var note = new NoteDTO
                {
                    Id = this.NotesData.Count,
                    DateCreated = DateTime.Now,
                    FirstName = "John",
                    LastName = "Wick",
                    Note = this.NoteText
                };

                NotesData.Add(note);
            }
            else
            {
                var note = NotesData[this.NoteId];
                note.Note = this.NoteText;
                NotesData[this.NoteId] = note;
            }
            this.NotesGrid.Reload();
            this.NoteId = -1;
            this.NoteText = string.Empty;
        }

        protected void OnEditButtonClick(NoteDTO note)
        {
            this.NoteId = note.Id;
            this.NoteText = note.Note;
        }

        private bool isNewNote()
        {
            return this.NoteId < 0;
        }

    }
}
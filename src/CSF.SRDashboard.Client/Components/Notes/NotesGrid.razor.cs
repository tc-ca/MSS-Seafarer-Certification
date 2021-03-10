namespace CSF.SRDashboard.Client.Components.Notes
{
    using System.Linq;
    using CSF.SRDashboard.Client.DTO;
    using Microsoft.AspNetCore.Components;
    using Radzen;
    using Radzen.Blazor;
    using System.Collections.Generic;

    public partial class NotesGrid
    {
        [Inject] 
        DialogService DialogService { get; set; }

        [Parameter]
        public List<NoteDTO> NotesData { get; set; }

        public RadzenGrid<NoteDTO> NotesRadzenGrid { get; set; }

        protected int NumberOfNotes;

        private int defaultPageSize = 5;

        private int notesPageSize;
        public void Refresh()
        {
            this.NotesRadzenGrid.Reload();
            InvokeAsync(StateHasChanged);
        }

        protected int NotesPageSize
        {
            get
            {
                return notesPageSize;
            }
            set
            {
                notesPageSize = value;

                StateHasChanged();

                if (NotesRadzenGrid != null)
                {
                    NotesRadzenGrid.Reload();
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            this.NotesPageSize = this.defaultPageSize;

            this.NotesRadzenGrid = new RadzenGrid<NoteDTO>();
        }
    }
}

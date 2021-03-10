namespace CSF.SRDashboard.Client.Components.Notes
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Localization;
    using Microsoft.JSInterop;
    using CSF.SRDashboard.Client.Models;
    using CSF.SRDashboard.Client.Utilities;
    using CSF.Web.Client.Shared;
    using Microsoft.AspNetCore.Components.Web;

    public partial class NotesEditor
    {
        [Inject]
        Radzen.DialogService dialogService { get; set; }

        [Inject] 
        public IJSRuntime JSRuntime { get; set; }

        [Inject] 
        public IStringLocalizer<Common> Localizer { get; set; }

        [Parameter]
        public NotesGrid Parent { get; set; }

        [Parameter]
        public Note Note { get; set; }

        private ElementReference NoteTextInput;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.FocusAsync(NoteTextInput);
            }
        }

        protected void CreateNote()
        {
            if (!string.IsNullOrEmpty(Note.Text))
            {
                if (this.isNewNote())
                {
                    var note = new Note
                    {
                        Id = Parent.NotesData.Count,
                        DateCreated = DateTime.Now,
                        FirstName = "John",
                        LastName = "Wick",
                        Text = Note.Text
                    };

                    Parent.NotesData.Add(note);
                }
                else
                {
                    var note = Parent.NotesData[Note.Id];
                    note.Text = Note.Text;
                    Parent.NotesData[Note.Id] = note;
                }
                Parent.Refresh();
                this.dialogService.Close(true);
            }
            else
            {
                this.dialogService.Close(false);
            }
        }
        protected void KeyUpEvent(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                this.CreateNote();
            }
        }

        protected void CloseDialog()
        {
            this.dialogService.Close(false);
        }

        private bool isNewNote()
        {
            return Note.Id < 0;
        }
    }
}

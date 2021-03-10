namespace CSF.SRDashboard.Client.Components.Notes
{
    using CSF.SRDashboard.Client.DTO;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;
    using System;

    public partial class NotesEditor
    {
        [Inject]
        Radzen.DialogService dialogService { get; set; }

        [Parameter]
        public NotesGrid Parent { get; set; }

        [Parameter]
        public NoteDTO Note { get; set; }

        protected void CreateNote()
        {
            if (!string.IsNullOrEmpty(Note.Note))
            {
                if (this.isNewNote())
                {
                    var note = new NoteDTO
                    {
                        Id = Parent.NotesData.Count,
                        DateCreated = DateTime.Now,
                        FirstName = "John",
                        LastName = "Wick",
                        Note = Note.Note
                    };

                    Parent.NotesData.Add(note);
                }
                else
                {
                    var note = Parent.NotesData[Note.Id];
                    note.Note = Note.Note;
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

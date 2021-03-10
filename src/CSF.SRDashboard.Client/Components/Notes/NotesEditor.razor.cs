namespace CSF.SRDashboard.Client.Components.Notes
{
    using CSF.SRDashboard.Client.Models;
    using Microsoft.AspNetCore.Components;
    using System;

    public partial class NotesEditor
    {
        [Inject]
        Radzen.DialogService dialogService { get; set; }

        [Parameter]
        public NotesGrid Parent { get; set; }

        [Parameter]
        public Note Note { get; set; }

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

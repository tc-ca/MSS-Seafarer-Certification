namespace CSF.SRDashboard.Client.Models
{
    using System;
    public class Note
    {
        public int Id { get; set; } = -1;

        public DateTime DateCreated { get; set; }

        public string DateOfCreated { get { return DateCreated.ToString("MMMM dd, yyyy"); } }

        public string Text { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

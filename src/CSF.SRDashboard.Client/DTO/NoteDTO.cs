using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO
{
    public class NoteDTO
    {
        public int Id { get; set; } = -1;

        public DateTime DateCreated { get; set; }

        public string DateOfCreated { get { return DateCreated.ToString("MMMM dd, yyyy"); } }

        public string Note { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}

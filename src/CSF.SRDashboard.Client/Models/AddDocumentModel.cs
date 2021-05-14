using DSD.MSS.Blazor.Components.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class AddDocumentModel
    {
        public List<SelectListItem> DocumentTypeList { get; set; }
        public List<string> Languages { get; set; }
        public string Description { get; set; }
        public int SelectValue { get; set; }
    public AddDocumentModel()
        {
            DocumentTypeList = new List<SelectListItem>();

            DocumentTypeList.Add(new SelectListItem()
            {
                Text = "MME Exam Report",
                Value = false
            });
            DocumentTypeList.Add(new SelectListItem()
            {
                Text = "Medical Report",
                Value = false
            });
            DocumentTypeList.Add(new SelectListItem()
            {
                Text = "Letter",
                Value = false
            });
            DocumentTypeList.Add(new SelectListItem()
            {
                Text = "Certificate",
                Value = false
            });
            DocumentTypeList.Add(new SelectListItem()
            {
                Text = "Other",
                Value = false
            });
            Languages = new List<string>()
            {
                "English",
                "French"
            };
            this.SelectValue = -1;
        }
    }
}

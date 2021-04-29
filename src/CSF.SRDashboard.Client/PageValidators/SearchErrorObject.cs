using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.PageValidators
{
    public enum ErrorType
    {
        CRITERIA,
        NO_RESULT
    }
    public class SearchErrorObject
    {

        public bool ErrorToggle { get; set; } = true;
        public ErrorType? Error { get; set; }


        public void HideError()
        {
            this.ErrorToggle = true;
        }
        public void ShowError()
        {
            this.ErrorToggle = false;
        }
    }
}

using CDNApplication.Models.PageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDNApplication.Utilities
{
    /// <summary>
    /// A class that is used to transfer data from one page to another
    /// </summary>
    public class SessionState
    {

        public UploadDocumentPageModel UploadDocumentPage { get; set; }

    }
}

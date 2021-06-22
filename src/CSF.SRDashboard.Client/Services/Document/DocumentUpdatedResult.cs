using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services.Document
{
    /// <summary>
    /// The result object for the update document info call.
    /// </summary>
    public class DocumentUpdatedResult
    {
        /// <summary>
        /// Gets  or sets the document id that was updated.
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Gets or sets whether the document was updated or not.
        /// </summary>
        public bool IsUpdated { get; set; }
    }
}

using CSF.SRDashboard.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services.WorkloadRequest
{
    public class ProcessingPhaseUtility
    {
        /// <summary>
        /// Gets Processing Phase text from table Processing Phase id
        /// </summary>
        public string FindProcessingPhaseById(RequestModel requestModel)
        {
            if (requestModel.Status.Equals(Constants.RequestStatuses[0].Id))
            {
                return Constants.ProcessingPhaseNew.Where(x => x.Id.Equals(requestModel.ProcessingPhase)).Single().Text;
            }
            else if (requestModel.Status.Equals(Constants.RequestStatuses[1].Id))
            {
                return Constants.ProcessingPhaseInProgress.Where(x => x.Id.Equals(requestModel.ProcessingPhase)).Single().Text;
            }
            else if (requestModel.Status.Equals(Constants.RequestStatuses[2].Id))
            {
                return Constants.ProcessingPhasePending.Where(x => x.Id.Equals(requestModel.ProcessingPhase)).Single().Text;
            }
            else if (requestModel.Status.Equals(Constants.RequestStatuses[3].Id))
            {
                return Constants.ProcessingPhaseComplete.Where(x => x.Id.Equals(requestModel.ProcessingPhase)).Single().Text;
            }
            else if (requestModel.Status.Equals(Constants.RequestStatuses[4].Id))
            {
                return Constants.ProcessingPhaseCancelled.Where(x => x.Id.Equals(requestModel.ProcessingPhase)).Single().Text;
            }
            else
            {
                return null;
            }
        }

    }
}

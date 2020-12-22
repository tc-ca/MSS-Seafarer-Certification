namespace CSF.Web.Client.Data.Services
{
    /// <summary>
    /// Interface for Generating a service request Id.
    /// </summary>
    public interface IMtoaRequestService
    {
        /// <summary>
        /// This method makes a request to MTOA and returns a request service Id.
        /// </summary>
        /// <returns>Returns an integer value of request service Id, returns -1 if not successful).</returns>
        public int GetServiceRequestIdFromMTOA();
    }
}

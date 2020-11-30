namespace CDNApplication.Data.DTO.MTAPI
{
    /// <summary>
    /// Expresses Service reqeust Created on MTOA.
    /// </summary>
    public class ServiceRequestCreationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRequestCreationResult"/> class.
        /// </summary>
        public ServiceRequestCreationResult()
        {
        }

        /// <summary>
        /// Gets or sets service request Id.
        /// </summary>
        public int ServiceRequestId { get; set; }
    }
}

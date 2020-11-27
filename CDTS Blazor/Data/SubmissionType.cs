using System.ComponentModel;

namespace CDNApplication.Data
{
    /// <summary>
    /// Enumeration for renewals.
    /// </summary>
    public enum SubmissionType
    {
        /// <summary>
        /// Defines the user is not renewing.
        /// </summary>
        [Description("NewCertificate")]
        NEW,

        /// <summary>
        /// Defines the user is renewing.
        /// </summary>
        [Description("RenewalCertificate")]
        RENEWAL,
    }
}

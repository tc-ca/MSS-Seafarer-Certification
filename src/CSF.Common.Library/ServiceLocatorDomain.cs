namespace CSF.Common.Library
{
    /// <summary>
    /// Enumerator for all web services in the solution.
    /// This enumerator is used in conjunction with the <see cref="ServiceLocator"/> to retrieve service endpoints from configuration.
    /// </summary>
    public enum ServiceLocatorDomain
    {
        /// <summary>
        /// TC DocumentService
        /// </summary>
        Document,

        /// <summary>
        /// MyTC Account
        /// </summary>
        Mtoa,

        /// <summary>
        /// MPDIS
        /// </summary>
        Mpdis,

        /// <summary>
        /// Gateway to MPDIS
        /// </summary>
        GatewayToMpdis,
    }
}
namespace CSF.Common.Library
{
    using System;

    /// <summary>
    /// This service is used to retrieve a service endpoint as indicated by the <see cref="ServiceLocatorDomain"/>.
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        /// Method that retrieves the service endpoint from applicatoin configuration as specified by <see cref="ServiceLocatorDomain"/>.
        /// </summary>
        /// <param name="serviceName">Name of your service as specified by <see cref="ServiceLocatorDomain"/>.</param>
        /// <returns>Service endpoint URL.</returns>
        Uri GetServiceUri(ServiceLocatorDomain serviceName);
    }
}
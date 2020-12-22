namespace CSF.Web.Client.Utilities
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// This service is used to retrieve a service endpoint as indicated by the <see cref="ServiceLocatorDomain"/>.
    /// </summary>
    public class ServiceLocator : IServiceLocator
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocator"/> class.
        /// </summary>
        /// <param name="config">Application configuration service.</param>
        /// <param name="logger">Logging service.</param>
        public ServiceLocator(IConfiguration config, ILogger<ServiceLocator> logger)
        {
            this.configuration = config;
            this.logger = logger;
        }

        /// <summary>
        /// Method that retrieves the service endpoint from applicatoin configuration as specified by <see cref="ServiceLocatorDomain"/>.
        /// </summary>
        /// <param name="serviceName">Name of your service as specified by <see cref="ServiceLocatorDomain"/>.</param>
        /// <returns>Service endpoint URL.</returns>
        public Uri GetServiceUri(ServiceLocatorDomain serviceName)
        {
            // Transform from Enum to string
            var key = serviceName.ToString();

            this.logger.LogInformation($"Locating Service URL with Key {key}");

            // Retrieve URL from Configuration
            var uri = this.configuration.GetSection("ServiceLocatorEndpoints")[key];

            this.logger.LogInformation($"Target Service URL is {uri}");

            if (uri == null)
            {
                throw new ArgumentNullException($"Uri key for {key} has not been configured. Add to it UserSecrets or appsettings.json");
            }

            return new Uri(uri);
        }
    }
}
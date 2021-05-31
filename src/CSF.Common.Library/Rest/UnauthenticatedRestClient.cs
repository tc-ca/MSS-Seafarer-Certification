namespace CSF.Common.Library
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// An unauthenticated Rest client that can be used by the application to make API calls.
    /// </summary>
    public class UnauthenticatedRestClient : AbstractRestClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthenticatedRestClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HttpClient.</param>
        /// <param name="serviceLocator">Service locator.</param>
        public UnauthenticatedRestClient(HttpClient httpClient, IServiceLocator serviceLocator) : base(httpClient, serviceLocator)
        {
        }

        protected override void ResetRestClientHeaders()
        {
            base.ResetRestClientHeaders();
            this.httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
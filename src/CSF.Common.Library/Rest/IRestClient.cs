namespace CSF.Common.Library
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface to for implementation of a REST client.
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// Makes a GET call to the specified API.
        /// </summary>
        /// <typeparam name="TReturnMessage">Object type returned by the API.</typeparam>
        /// <param name="serviceName">Name of the service as specified by <see cref="ServiceLocatorDomain"/>.</param>
        /// <param name="path">Path to the API being called on the service.</param>
        /// <returns>Return bject as specified by the API.</returns>
        Task<TReturnMessage> GetAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path)
            where TReturnMessage : class, new();

        /// <summary>
        /// Makes a POST call to the specified API.
        /// </summary>
        /// <typeparam name="TReturnMessage">Object type returned by the API.</typeparam>
        /// <param name="serviceName">Name of the service as specified by <see cref="ServiceLocatorDomain"/>.</param>
        /// <param name="path">Path to the API being called on the service.</param>
        /// <param name="dataObject">Object to post to the API.</param>
        /// <returns>Return object as specified by the API.</returns>
        Task<TReturnMessage> PostAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path, object dataObject = null)
            where TReturnMessage : class, new();

        /// <summary>
        /// Makes a POST call to the specified API.
        /// </summary>
        /// <typeparam name="TReturnMessage">Object type returned by the API.</typeparam>
        /// <param name="restClientRequestOptions">the rest client request options.</param>
        /// <returns>Return object as specified by the API.</returns>
        Task<TReturnMessage> PostAsync<TReturnMessage>(RestClientRequestOptions restClientRequestOptions)
            where TReturnMessage : class, new();
        /// <summary>
        /// Makes a PUT call to the specified API.
        /// </summary>
        /// <typeparam name="TReturnMessage">Object type returned by the API.</typeparam>
        /// <param name="serviceName">Name of the service as specified by <see cref="ServiceLocatorDomain"/>.</param>
        /// <param name="path">Path to the API being called on the service.</param>
        /// <param name="dataObject">Object to put to the API.</param>
        /// <returns>Return bject as specified by the API.</returns>
        Task<TReturnMessage> PutAsync<TReturnMessage>(ServiceLocatorDomain serviceName, string path, object dataObject = null)
            where TReturnMessage : class, new();
        /// <summary>
        /// Makes a PUT call to the specified API.
        /// </summary>
        /// <typeparam name="TReturnMessage">Object type returned by the API.</typeparam>
        /// <param name="restClientRequestOptions">the rest client request options.</param>
        /// <returns>Return object as specified by the API.</returns>
        Task<TReturnMessage> PutAsync<TReturnMessage>(RestClientRequestOptions restClientRequestOptions)
            where TReturnMessage : class, new();


        /// <summary>
        /// This method is used when the call does not return a DTO rather just a status. When a call returns a DTO, use the PutAsync method.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="path"></param>
        /// <param name="dataObject"></param>
        /// <returns>returns a positive number when successful, returns a negative number when failed</returns>
        Task<int> UpdateAsync(ServiceLocatorDomain serviceName, string path, object dataObject = null);


        /// <summary>
        /// Makes a DELETE call to the specified API.
        /// </summary>
        /// <param name="serviceName">Name of the service as specified by <see cref="ServiceLocatorDomain"/>.</param>
        /// <param name="path">Path to the API being called on the service.</param>
        /// <returns>A boolean value representing result of operation.</returns>
        Task<bool> DeleteAsync(ServiceLocatorDomain serviceName, string path);
    }
}
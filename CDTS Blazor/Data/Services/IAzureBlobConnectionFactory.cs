namespace CDNApplication.Data.Services
{
    using System.Threading.Tasks;
    using Microsoft.Azure.Storage.Blob;

    /// <summary>
    /// The interface to define the azure blob connection factore.
    /// </summary>
    public interface IAzureBlobConnectionFactory
    {
        /// <summary>
        /// Gets the blob container.
        /// </summary>
        /// <param name="container">The container connection string.</param>
        /// <returns>The azure cloud blob storage.</returns>
        Task<CloudBlobContainer> GetBlobContainer(string container = null);
    }
}

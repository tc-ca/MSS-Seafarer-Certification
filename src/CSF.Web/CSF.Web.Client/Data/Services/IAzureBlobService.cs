namespace CSF.Web.Client.Data.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BlazorInputFile;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Azure.Storage.Blob;

    /// <summary>
    /// Interface for the Azure blob service storage.
    /// </summary>
    public interface IAzureBlobService
    {
        /// <summary>
        /// Uploads a single file to the azure blob storage.
        /// </summary>
        /// <param name="file">The file to upload.</param>
        /// <param name="container">The container to connect to..</param>
        /// <returns>The uploaded blob.</returns>
        Task<CloudBlockBlob> UploadFileAsync(IFormFile file, string container = null);
    }
}

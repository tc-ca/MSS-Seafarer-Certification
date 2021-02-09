namespace CSF.Common.Library.Azure
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Azure.Storage.Blob;

    /// <summary>
    /// Represents the Azure blob service.
    /// </summary>
    public class AzureBlobService : IAzureBlobService
    {
        private readonly IAzureBlobConnectionFactory azureBlobConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureBlobService"/> class.
        /// </summary>
        /// <param name="azureBlobConnectionFactory">The Azure blob connection factory.</param>
        public AzureBlobService(IAzureBlobConnectionFactory azureBlobConnectionFactory)
        {
            this.azureBlobConnectionFactory = azureBlobConnectionFactory;
        }

        /// <inheritdoc/>
        public async Task<CloudBlockBlob> UploadFileAsync(IFormFile file, string container = null)
        {
            // Perhaps we can fail more gracefully then just throwing an exception
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var blobContainer = await this.azureBlobConnectionFactory.GetBlobContainer().ConfigureAwait(false);

            var blobName = AzureBlobService.UniqueFileName(file.FileName);

            // Create the blob to hold the data
            var blob = blobContainer.GetBlockBlobReference(blobName);

            // Send the file to the cloud storage
            using (var stream = file.OpenReadStream())
            {
                await blob.UploadFromStreamAsync(stream).ConfigureAwait(false);
            }

            return blob;
        }

        private static string UniqueFileName(string currentFileName)
        {
            string ext = Path.GetExtension(currentFileName);

            string nameWithNoExt = Path.GetFileNameWithoutExtension(currentFileName);

            return string.Format(CultureInfo.InvariantCulture, "{0}_{1}{2}", nameWithNoExt, DateTime.UtcNow.Ticks, ext);
        }
    }
}

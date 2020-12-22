namespace CSF.Web.Client.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BlazorInputFile;
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

        /// <inheritdoc/>
        public async Task<List<CloudBlockBlob>> UploadMultipleFilesAsync(IFileListEntry[] files, string container = null)
        {
            // Perhaps we can fail more gracefully then just throwing an exception
            if (files == null)
            {
                throw new ArgumentNullException(nameof(files));
            }

            // Get the container
            var blobContainer = await this.azureBlobConnectionFactory.GetBlobContainer(container).ConfigureAwait(false);

            var list = new List<CloudBlockBlob>();

            for (int i = 0; i < files.Length; i++)
            {
                // Create the blob to hold the data
                var blob = blobContainer.GetBlockBlobReference(UniqueFileName(files[i].Name));

                // Send the file to the cloud
                using (StreamContent streamContent = new StreamContent(files[i].Data))
                {
                    var stream = await streamContent.ReadAsStreamAsync().ConfigureAwait(false);

                    await blob.UploadFromStreamAsync(stream).ConfigureAwait(false);
                }

                list.Add(blob);
            }

            return list;
        }

        private static string UniqueFileName(string currentFileName)
        {
            string ext = Path.GetExtension(currentFileName);

            string nameWithNoExt = Path.GetFileNameWithoutExtension(currentFileName);

            return string.Format(CultureInfo.InvariantCulture, "{0}_{1}{2}", nameWithNoExt, DateTime.UtcNow.Ticks, ext);
        }
    }
}

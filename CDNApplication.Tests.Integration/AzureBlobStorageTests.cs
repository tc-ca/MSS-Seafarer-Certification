namespace CDNApplication.Tests.Integration
{
    using System;
    using System.IO;
    using System.Text;
    using CDNApplication.Data.Services;
    using CDNApplication.Tests.Integration.Services;
    using Microsoft.AspNetCore.Http;
    using Xunit;

    public class AzureBlobStorageTests
    {
        private readonly AzureBlobService azureBlobService;

        public AzureBlobStorageTests()
        {
            this.azureBlobService = InitializeServices.GetAzureBlobService();
        }

        [Fact]
        public async void UploadFileAsync_UploadTestFile_ReturnsNotNull(string fileName)
        {
            FormFile uploadTestFile;
            using (var memoryStream = new MemoryStream())
            {
                // Arrange
                byte[] buffer = Encoding.Default.GetBytes(
                    "Test file for UploadFileAsync_UploadTestFile_ReturnsNotNull() method.");
                memoryStream.Write(buffer, 0, buffer.Length);
                uploadTestFile =
                    new FormFile(
                        memoryStream,
                        0,
                        memoryStream.Length,
                        null,
                        string.Format("uploadtestfile_{0}", new Random().Next().ToString()))
                        {
                            Headers = new HeaderDictionary(), ContentType = "application/txt"
                        };

                // Act
                var result = await this.azureBlobService.UploadFileAsync(uploadTestFile);

                // Assert
                Assert.NotNull(result);
            }
        }
    }
}
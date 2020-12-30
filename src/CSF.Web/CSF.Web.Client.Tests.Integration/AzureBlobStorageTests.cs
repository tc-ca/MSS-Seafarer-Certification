namespace CSF.Web.Client.Tests.Integration
{
    using System;
    using System.IO;
    using System.Text;
    using CSF.Web.Client.Data.Services;
    using CSF.Web.Client.Tests.Integration.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Xunit;

    public class AzureBlobStorageTests
    {
        private readonly IAzureBlobService azureBlobService;

        public AzureBlobStorageTests()
        {
            var mockConfiguration = Mock.Of<IConfiguration>(x => x.GetSection("AzureKeyVaultSettings")["KeyVaultServiceEndpoint"] == "https://kv-seafarer-dev.vault.azure.net/");
            var azureKeyVaultService = new AzureKeyVaultService(mockConfiguration);
            var azureBlobConnectionFactory = new AzureBlobConnectionFactory(azureKeyVaultService);
            this.azureBlobService = new AzureBlobService(azureBlobConnectionFactory);
        }

        [Fact]
        public async Task UploadFileAsync_UploadTestFileWhenFileIsNull_ThrowsArgumentNullException()
        {
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.azureBlobService.UploadFileAsync(null));
        }

        [Fact]
        public async void UploadFileAsync_UploadTestFile_ReturnsNotNull()
        {
            using (var memoryStream = new MemoryStream())
            {
                // Arrange
                byte[] buffer = Encoding.Default.GetBytes(
                    "Test file for UploadFileAsync_UploadTestFile_ReturnsNotNull() method.");
                memoryStream.Write(buffer, 0, buffer.Length);
                FormFile uploadTestFile =
                    new FormFile(
                        memoryStream,
                        0,
                        memoryStream.Length,
                        null,
                        string.Format("uploadTestFile_{0}", new Random().Next().ToString()))
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
using CDNApplication.Data.Services;
using CDNApplication.Tests.Integration.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using Xunit;

namespace CDNApplication.Tests.Integration
{
    public class AzureBlobStorageTests
    {
        private readonly AzureBlobService azureBlobService;

        public AzureBlobStorageTests()
        {
            azureBlobService = InitializeServices.GetAzureBlobService();
        }

        [Theory]
        [InlineData("random-text.txt")]
        public async void UploadFileAsync_UploadTextFile_ReturnsNotNull(string fileName)
        {
            // Arrange
            var root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;

            string newPath = Path.GetFullPath(Path.Combine(root.FullName, @"..\DummyData\", fileName));

            using var stream = File.OpenRead(newPath);

            var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/" + Path.GetExtension(newPath).Replace(".", "")
            };

            // Act
            var result = await azureBlobService.UploadFileAsync(file, "unittests");

            // Assert
            Assert.NotNull(result);
        }
    }
}

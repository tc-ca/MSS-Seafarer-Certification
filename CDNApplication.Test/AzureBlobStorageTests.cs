using BlazorInputFile;
using CDNApplication.Data.Services;
using CDNApplication.Test.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CDNApplication.Test
{
    public class AzureBlobStorageTests
    {
        private readonly AzureBlobService azureBlobService;

        public AzureBlobStorageTests()
        {
            azureBlobService = Service.GetAzureBlobService();
        }

        [Theory]
        [InlineData("random-text.txt")]
        [InlineData("Azure-EBook.pdf")]
        [InlineData("happy-birthday.png")]
        public async void UploadFileToAzureBlob(string fileName)
        {

            var root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;

            string newPath = Path.GetFullPath(Path.Combine(root.FullName, @"..\DummyData\", fileName));

            Assert.True(File.Exists(newPath));

            using var stream = File.OpenRead(newPath);

            var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/" + Path.GetExtension(newPath).Replace(".", "")
            };

            var result = await azureBlobService.UploadFileAsync(file, "unittests");

            Assert.NotNull(result);

        }

        [Fact]
        public async void UploadFilesToAzureBlob()
        {

            var root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;

            var fileName1 = "random-text.txt";

            var fileName2 = "happy-birthday.png";

            var formFiles = new List<FormFile>();

            string newPath1 = Path.GetFullPath(Path.Combine(root.FullName, @"..\DummyData\", fileName1));

            string newPath2 = Path.GetFullPath(Path.Combine(root.FullName, @"..\DummyData\", fileName2));

            var pathArr = new List<string> { newPath1, newPath2 };

            foreach (var item in pathArr)
            {
                Assert.True(File.Exists(item));
            }

            foreach (var item in pathArr)
            {
                using var stream = File.OpenRead(item);

                formFiles.Add(new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/" + Path.GetExtension(newPath1).Replace(".", "")
                });
            }

            var t = new List<IFileListEntry>();

            //var result = await azureBlobService.UploadMultipleFilesAsync(formFiles, "unittests");


            //Assert.NotNull(result);

        }

    }
}

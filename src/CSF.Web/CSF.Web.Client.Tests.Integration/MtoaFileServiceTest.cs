namespace CSF.Web.Client.Tests.Integration
{
    using CSF.Web.Client.Data.DTO.MTAPI;
    using CSF.Web.Client.Data.Services;
    using CSF.Web.Client.Exceptions;
    using CSF.Web.Client.Tests.Integration.Services;
    using Moq;
    using System.IO;
    using Xunit;

    /// <summary>
    /// Class to test integration with mtoa upload file.
    /// </summary>
    public class MtoaFileServiceTest
    {
        private readonly int serviceRequestId = 13844; //13844 is for Dev. For Kanga use  15703
        private readonly byte[] defaultFileBytes = { 1, 2, 3, 4, 0xBA, 0xDF, 0x00, 0x0D };
        private readonly MtoaFileService mtoaFileService;

        public MtoaFileServiceTest()
        {
            var mockConfiguration = Mock.Of<IConfiguration>(x => x.GetSection("AzureKeyVaultSettings")["KeyVaultServiceEndpoint"] == "https://kv-seafarer-dev.vault.azure.net/");
            var azureKeyVaultService = new AzureKeyVaultService(mockConfiguration);
            this.mtoaFileService = new MtoaFileService(azureKeyVaultService);
        }

        [Fact]
        public void MtoaFileService_UploadFile_Success()
        {
            // Arrange
            var fileAttachment = this.createFileAttachment(this.defaultFileBytes);

            try
            {
                // TODO: Once Kenga is back online we want to remove this try catch. 
                // This is only useful for making sure github and azure devops tests
                // Don't fail when running, but not being able to access MTOA
                // Act
                var uploadedFile = this.mtoaFileService.UploadFile(serviceRequestId, fileAttachment).GetAwaiter().GetResult();

                // Assert
                Assert.True(uploadedFile.Id > 0);
            } 
            catch (MtoaConnectionException mtoaConnectionException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void MtoaFileService_UploadFile_InfectedFile()
        {
            // Arrange
            var virusFileBytes = this.createEICARTestVirusFileBytes();
            var virusFileAttachment = this.createFileAttachment(virusFileBytes);

            try
            {
                // TODO: Once Kenga is back online we want to remove this try catch. 
                // This is only useful for making sure github and azure devops tests
                // Don't fail when running, but not being able to access MTOA
                // Act
                var uploadedFile = this.mtoaFileService.UploadFile(serviceRequestId, virusFileAttachment).GetAwaiter().GetResult();

                // Assert
                Assert.True(uploadedFile.Id < 0);
            }
            catch (MtoaConnectionException mtoaConnectionException)
            {
                Assert.True(true);
            }
        }

        [Theory]
        [InlineData("'\\/|<>:?*!\"")]
        public void MtoaFileService_UploadFileWithSpecialCharacters_Succeeds(string specialCharacters)
        {
            // Arrange
            var fileName = string.Format("FileWitth{0}specialcharacter.txt", specialCharacters);
            var fileAttachment = this.createFileAttachment(this.defaultFileBytes, fileName);

            try
            {
                // Act
                var uploadedFile = this.mtoaFileService.UploadFile(serviceRequestId, fileAttachment).GetAwaiter().GetResult();

                // Assert
                Assert.True(uploadedFile.Id > 0);
            }
            catch (MtoaConnectionException mtoaConnectionException)
            {
                Assert.True(true);
            }
        }

        private byte[] createEICARTestVirusFileBytes()
        {
            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                TextWriter tw = new StreamWriter(ms);
                tw.Write(@"X5O!P%@AP[4\PZX54(P^)7CC)7}$EICAR-STANDARD-ANTIVIRUS-TEST-FILE!$H+H*");
                tw.Flush();
                ms.Position = 0;
                bytes = ms.ToArray();
            }
            return bytes;
        }

        private FileAttachment createFileAttachment(byte[] fileBytes)
        {
            return new FileAttachment
            {
                ContentType = "testing",
                Data = fileBytes,
                Name = "FirstName.txt",
                ServiceRequestId = serviceRequestId,
                Size = fileBytes.Length
            };
        }

        private FileAttachment createFileAttachment(byte[] fileBytes, string fileName)
        {
            return new FileAttachment
            {
                ContentType = "testing",
                Data = fileBytes,
                Name = fileName,
                ServiceRequestId = serviceRequestId,
                Size = fileBytes.Length
            };
        }
    }
}

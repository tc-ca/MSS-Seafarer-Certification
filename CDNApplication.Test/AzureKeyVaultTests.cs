using CDNApplication.Data.Services;
using CDNApplication.Test.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CDNApplication.Test
{
    public class AzureKeyVaultTests
    {

        private AzureKeyVaultService azureKeyVaultService;

        public AzureKeyVaultTests()
        {
            azureKeyVaultService = Service.GetAzureKeyVaultService();
        }

        [Theory]
        [InlineData("BlobStorage")]
        public void GetSerectByName_ReturnsSerect(string serectName)
        {
            var serect = azureKeyVaultService.GetSecretByName(serectName);

            Assert.NotNull(serect);
        }

        [Fact]
        public void GetSerects_ReturnsAListOfSerects()
        {
            var serect = azureKeyVaultService.GetListOfSecrets();

            Assert.NotNull(serect);

            Assert.NotEmpty(serect);

        }

    }
}

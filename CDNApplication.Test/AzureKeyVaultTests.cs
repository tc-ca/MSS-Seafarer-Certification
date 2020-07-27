using CDNApplication.Data.Services;
using CDNApplication.Test.Services;
using Microsoft.Azure.KeyVault.Models;
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
            azureKeyVaultService = InitializeServices.GetAzureKeyVaultService();
        }

        [Theory]
        [InlineData("BlobStorage")]
        public void GetSerectByName_ReturnsSecret(string serectName)
        {

            var serect = azureKeyVaultService.GetSecretByName(serectName);

            Assert.NotEmpty(serect);

        }

        [Fact]
        public void GetSerects_ReturnsAListOfSerects()
        {

            var serect = azureKeyVaultService.GetListOfSecrets();

            Assert.NotEmpty(serect);

        }

    }
}

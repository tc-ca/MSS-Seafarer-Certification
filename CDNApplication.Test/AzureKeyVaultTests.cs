using CDNApplication.Data.Services;
using CDNApplication.Test.Services;
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
        public void GetSecretByName_GetBlobStorageSecret_ReturnsNotEmpty(string secretName)
        {
            var secret = azureKeyVaultService.GetSecretByName(secretName);

            Assert.NotEmpty(secret);
        }

        [Fact]
        public void GetListOfSecrets_GetAllSecrets_ReturnsNotEmpty()
        {
            var secret = azureKeyVaultService.GetListOfSecrets();

            Assert.NotEmpty(secret);
        }
    }
}

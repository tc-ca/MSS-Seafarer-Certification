namespace CDNApplication.Tests.Integration
{
    using CDNApplication.Data.Services;
    using CDNApplication.Tests.Integration.Services;
    using Xunit;

    public class AzureKeyVaultTests
    {
        private AzureKeyVaultService azureKeyVaultService;

        public AzureKeyVaultTests()
        {
            this.azureKeyVaultService = InitializeServices.GetAzureKeyVaultService();
        }

        [Fact]
        public void GetListOfSecrets_GetAllSecrets_ReturnsNotEmpty()
        {
            var secret = this.azureKeyVaultService.GetListOfSecrets();

            Assert.NotEmpty(secret);
        }

        [Theory]
        [InlineData("BlobStorage")]
        public void GetSecretByName_GetBlobStorageSecret_ReturnsNotEmpty(string secretName)
        {
            var secret = this.azureKeyVaultService.GetSecretByName(secretName);

            Assert.NotEmpty(secret);
        }
    }
}
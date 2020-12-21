namespace CDNApplication.Tests.Integration
{
    using CDNApplication.Data.Services;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Xunit;

    public class AzureKeyVaultTests
    {
        private readonly AzureKeyVaultService azureKeyVaultService;

        public AzureKeyVaultTests()
        {
            var mockConfiguration = Mock.Of<IConfiguration>(x => x.GetSection("AzureKeyVaultSettings")["KeyVaultServiceEndpoint"] == "https://kv-seafarer-dev.vault.azure.net/");
            this.azureKeyVaultService = new AzureKeyVaultService(mockConfiguration);
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
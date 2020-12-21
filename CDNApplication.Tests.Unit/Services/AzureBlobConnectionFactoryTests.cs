namespace CDNApplication.Tests.Unit.Services
{
    using CDNApplication.Data.Services;
    using Moq;
    using Xunit;

    public class AzureBlobConnectionFactoryTests
    {
        private IAzureBlobConnectionFactory azureBlobConnectionFactory;

        public  AzureBlobConnectionFactoryTests()
        {
            var azureKeyVaultServiceMock = new Mock<IKeyVaultService>();
            this.azureBlobConnectionFactory = new AzureBlobConnectionFactory(azureKeyVaultServiceMock.Object);
        }

        [Fact]
        public void GetBlobContainer_WhenNoConnectionString_ThrowsCloudStorageAccountConnectionStringException()
        {
            // Assert
            Assert.ThrowsAsync<CloudStorageAccountConnectionStringException>(() => this.azureBlobConnectionFactory.GetBlobContainer());
        }
    }
}

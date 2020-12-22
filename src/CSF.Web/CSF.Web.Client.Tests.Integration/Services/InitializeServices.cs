namespace CSF.Web.Client.Tests.Integration.Services
{
    using CSF.Web.Client.Data.Services;

    public class InitializeServices
    {
        public static AzureBlobService GetAzureBlobService()
        {
            var az = new AzureBlobConnectionFactory(GetAzureKeyVaultService());

            return new AzureBlobService(az);
        }

        public static AzureKeyVaultService GetAzureKeyVaultService()
        {
            return new AzureKeyVaultService("https://kv-seafarer-dev.vault.azure.net/");
        }
    }
}
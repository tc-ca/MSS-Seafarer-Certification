using CDNApplication.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDNApplication.Test.Services
{
    public class Service
    {

        public static AzureKeyVaultService GetAzureKeyVaultService()
        {
            return new AzureKeyVaultService("https://kv-seafarer-dev.vault.azure.net/");
        }

        public static AzureBlobService GetAzureBlobService()
        {
            var az = new AzureBlobConnectionFactory(GetAzureKeyVaultService());

            return new AzureBlobService(az);
        }

    }
}

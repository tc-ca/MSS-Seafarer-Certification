namespace CSF.Common.Library
{
    using CSF.Common.Library.Azure;
    using CSF.Common.Library.Rest;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net.Http;

    public static class StartupConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IServiceLocator, ServiceLocator>();
            services.AddSingleton<IRestClient, UnauthenticatedRestClient>();
            services.AddTransient<IKeyVaultService, AzureKeyVaultService>();
            services.AddScoped<IAzureBlobService, AzureBlobService>();
            services.AddTransient<IAzureBlobConnectionFactory, AzureBlobConnectionFactory>();
        }
    }
}

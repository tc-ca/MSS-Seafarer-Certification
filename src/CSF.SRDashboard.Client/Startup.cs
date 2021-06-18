using CSF.API.Data.Contexts;
using CSF.API.Services.Repositories;
using CSF.Common.Library;
using CSF.Common.Library.Azure;
using CSF.SRDashboard.Client.Graph;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.PageValidators;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Services.Document;
using CSF.SRDashboard.Client.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MPDIS.API.Wrapper.Services.MPDIS;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using Radzen;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace CSF.SRDashboard.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi(GraphConstants.Scopes)
                .AddInMemoryTokenCaches();

            services.AddAuthorization(options =>
            {
                // By default, all incoming requests will be authorized according to the default policy
                options.FallbackPolicy = options.DefaultPolicy;
            });

            services.AddRazorPages()
                .AddMicrosoftIdentityUI();
            services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();

            services.AddSingleton<HttpClient>();
            services.AddSingleton<IServiceLocator, ServiceLocator>();
            services.AddSingleton<IMpdisService, MpdisService>();
            services.AddSingleton<IKeyVaultService, AzureKeyVaultService>();
            services.AddSingleton<IGatewayService, GatewayService>();
            services.AddSingleton<IRestClient, UnauthenticatedRestClient>();
            services.AddSingleton<IRestClient, GatewayRestClient>();
            services.AddSingleton<IDocumentService, DocumentService>();
            services.AddSingleton<IWorkLoadManagementService, WorkLoadManagementService>();

            services.AddTransient<IAzureBlobConnectionFactory, AzureBlobConnectionFactory>();
            services.AddTransient<IClientXrefDocumentRepository, ClientXrefDocumentRepository>();
            services.AddTransient<IKeyVaultService, AzureKeyVaultService>();
            services.AddTransient<IValidator<ApplicantSearchCriteria>, SearchValidator>();
            services.AddTransient<IMtoaArtifactService, MtoaArtifactService>();
            services.AddTransient<IValidator<AddDocumentModel>, AddAttachmentValidator>();

            services.AddScoped<IAzureBlobService, AzureBlobService>();
            services.AddScoped<SessionState>();
            services.AddScoped<DialogService>();
            services.AddScoped<RequestGridsModel>();
            services.AddScoped<IUserGraphApiService, UserGraphApiService>();

            services.AddApplicationInsightsTelemetry(Configuration.GetSection("ApplicationInsights:Instrumentationkey").Value);

            services.AddHttpContextAccessor();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var keyVault = serviceProvider.GetService<IKeyVaultService>();

            services.AddDbContext<ClientDBContext>(options =>
            {

                options.UseNpgsql(keyVault.GetSecretByName("DocumentStorageClientDbDatabase"));

            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            var supportedCultures = new List<CultureInfo> {
                new CultureInfo("en"),
                new CultureInfo("fr")
            };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IKeyVaultService kvService, IConfiguration appConfiguration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRequestLocalization();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            SetApplicationSecretsFromKeyVault(kvService, appConfiguration);
        }

        private void SetApplicationSecretsFromKeyVault(IKeyVaultService kvService, IConfiguration appConfiguration)
        {
            var secretName = appConfiguration.GetSection("AzureKeyVaultSettings")["SecretNames:GatewayToken"];
            var token = kvService.GetSecretByName(secretName);
            appConfiguration.GetSection("AzureKeyVaultSettings")["SecretNames:GatewayToken"] = token;
        }
    }
}

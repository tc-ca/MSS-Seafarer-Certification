using CSF.Common.Library;
using CSF.Common.Library.Azure;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.PageValidators;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
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
            var updatedAzureAD = ConfigurationHelperService.UpdateConfigurationForAzureAD(Configuration);

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApp(updatedAzureAD)
                    .EnableTokenAcquisitionToCallDownstreamApi(new[] { "User.Read" })
                    .AddInMemoryTokenCaches();

            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IServiceLocator, ServiceLocator>();
            services.AddSingleton<IMpdisService, MpdisService>();
            services.AddSingleton<IGatewayService, GatewayService>();
            services.AddSingleton<IRestClient, UnauthenticatedRestClient>();
            services.AddSingleton<IRestClient, GatewayRestClient>();
            services.AddSingleton<IWorkLoadManagementService, WorkLoadManagementService>();

            services.AddTransient<IKeyVaultService, AzureKeyVaultService>();
            services.AddTransient<IValidator<ApplicantSearchCriteria>, SearchValidator>();

            services.AddTransient<IMtoaArtifactService, MtoaArtifactService>();
            //services.AddTransient<IUserGraphApiService, UserGraphApiService>();
            services.AddTransient<IUserGraphApiService, MockUserGraphApi>();

            services.AddScoped<SessionState>();
            services.AddScoped<DialogService>();
            services.AddScoped<RequestGridsModel>();
            services.AddApplicationInsightsTelemetry(Configuration.GetSection("ApplicationInsights:Instrumentationkey").Value);

            services.AddHttpContextAccessor();

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

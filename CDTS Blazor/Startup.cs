namespace CDNApplication
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using CDNApplication.Data.Services;
    using CDNApplication.Middleware;
    using CDNApplication.Models;
    using CDNApplication.Models.PageModels;
    using CDNApplication.PageValidators;
    using CDNApplication.Services;
    using CDNApplication.Utilities;
    using CDNApplication.Views;
    using FluentValidation;
    using GoC.WebTemplate.Components.Core.Services;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using CustomRequestCultureProvider = CDNApplication.Utilities.CustomRequestCultureProvider;

    /// <summary>
    ///     The startup class for the Blazor application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The startup configuration options.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        ///     Gets the configuration options.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// Refer to this article for correct order of middleware calls: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.1.
        /// </summary>
        /// <param name="app">This object corresponds to the current running application.</param>
        /// <param name="env">Our web hosting environment.</param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

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

            app.UseStaticFiles();
            app.UsePageSettingsMiddleware();
            app.UseRequestLocalization(); // if GoC.WebTemplate-Components.Core (in NuGet) >= v2.1.1
            app.UseRouting();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapBlazorHub();
                        endpoints.MapFallbackToPage("/_Host");
                    });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </summary>
        /// <param name="services">Use to set services used by application.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-CA"),
                new CultureInfo("fr-CA"),
            };

            services.AddRazorPages();
            services.AddServerSideBlazor();
            
            services.AddScoped<UploadDocumentsStepper>();
            services.AddScoped<UploadDocumentPageModel>();
            services.AddScoped<SessionState>();
            
            services.AddTransient<LayoutViewModel>();
            services.AddTransient<IValidator<UploadDocumentPageModel>, UploadDocumentValidator>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-CA");
                options.SupportedUICultures = supportedCultures;
                options.SupportedCultures = supportedCultures;
                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Add(new CustomRequestCultureProvider());
            });

            services.AddSingleton(new AzureKeyVaultService(this.Configuration.GetSection("AzurePublicUrlEndpoints")["KeyVaultService"]));
            services.AddTransient<IAzureBlobConnectionFactory, AzureBlobConnectionFactory>();
            services.AddScoped<IAzureBlobService, AzureBlobService>();
            services.AddSingleton<SessionStateModel>();
            services.AddSingleton<MtoaEmailService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ISessionManager, SessionManager>();
            services.AddModelAccessor();
            services.ConfigureGoCTemplateRequestLocalization(); // if GoC.WebTemplate-Components.Core (in NuGet) >= v2.1.1
            services.AddApplicationInsightsTelemetry();
        }

    }
}
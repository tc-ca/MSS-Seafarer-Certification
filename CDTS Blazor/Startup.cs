namespace CDNApplication
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
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
    using Microsoft.AspNetCore.Http;
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
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </summary>
        /// <param name="services">Use to set services used by application.</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Enable anti-forgery
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-CA"),
                new CultureInfo("fr-CA"),
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-CA");

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<SessionState>();
            services.AddSingleton<UploadDocumentsStepper>();
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

            services.AddSingleton(new AzureKeyVaultService("https://kv-seafarer-dev.vault.azure.net/"));
            services.AddTransient<IAzureBlobConnectionFactory, AzureBlobConnectionFactory>();
            services.AddScoped<IAzureBlobService, AzureBlobService>();
            services.AddSingleton<SessionStateModel>();
            services.AddHttpContextAccessor();
            services.AddScoped<ISessionManager, SessionManager>();
            services.AddHttpContextAccessor();
            services.AddModelAccessor();
            services
                .ConfigureGoCTemplateRequestLocalization(); // if GoC.WebTemplate-Components.Core (in NuGet) >= v2.1.1

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                options.HttpsPort = 443;
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">This object corresponds to the current running application.</param>
        /// <param name="env">Our web hosting environment.</param>
        /// <param name="antiForgery">Anti Forgery settings.</param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiForgery)
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
                app.UseHttpsRedirection();
            }


            app.Use(next => context =>
            {
                var path = context.Request.Path.Value;

                if (string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(path, "/api", StringComparison.OrdinalIgnoreCase))
                {
                    // The request token can be sent as a JavaScript-readable cookie
                    var tokens = antiForgery.GetAndStoreTokens(context);

                    // Set the antiForgery token
                    context.Response.Cookies.Append(
                        "XSRF-TOKEN",
                        tokens.RequestToken,
                        new CookieOptions { HttpOnly = false });
                }

                return next(context);
            });

            app.UseStaticFiles();
            app.UsePageSettingsMiddleware();
            app.UseRequestLocalization(); // if GoC.WebTemplate-Components.Core (in NuGet) >= v2.1.1
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
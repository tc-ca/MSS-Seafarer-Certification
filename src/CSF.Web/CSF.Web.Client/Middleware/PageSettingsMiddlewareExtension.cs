namespace CSF.Web.Client.Middleware
{
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Extension methods for the <see cref="PageSettingsMiddleware"/> class.
    /// </summary>
    public static class PageSettingsMiddlewareExtension
    {
        /// <summary>
        /// Configures the builder to use the PageSettingsMiddleware.
        /// </summary>
        /// <param name="builder">The ApplicationBuilder.</param>
        /// <returns>The builder.</returns>
        public static IApplicationBuilder UsePageSettingsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PageSettingsMiddleware>();
        }
    }
}

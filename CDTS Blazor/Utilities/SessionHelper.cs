namespace CDNApplication.Utilities
{
    using System;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// The application's session helper.
    /// </summary>
    public sealed class SessionHelper
    {
        // TODO: This should be in a config file
        private const string DEFAULTLANGUAGE = "en";
        private const int LanguageIndexInPath = 1;

        /// <summary>
        /// Gets the language from the http context.
        /// </summary>
        /// <param name="context">the http context.</param>
        /// <returns>the language code.</returns>
        public static string GetLanguageFromContext(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (SessionHelper.IsContextPathEmpty(context))
            {
                return DEFAULTLANGUAGE;
            }

            return GetLanguageFromPath(context.Request.Path.Value);
        }

        private static bool IsContextPathEmpty(HttpContext context)
        {
            return !context.Request.Path.HasValue;
        }

        /// <summary>
        /// Gets the language from the uri path.
        /// </summary>
        /// <param name="path">the uri path.</param>
        /// <returns>the language code.</returns>
        public static string GetLanguageFromPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return DEFAULTLANGUAGE;
            }

            var pathSubDirectories = path.Split('/');
            if (SessionHelper.DoesURLPathContainOnlyOneSubdirectory(pathSubDirectories))
            {
                return DEFAULTLANGUAGE;
            }

            if (SessionHelper.IsSecondPathInURLNullOrEmpty(pathSubDirectories))
            {
                return DEFAULTLANGUAGE;
            }

            return pathSubDirectories[LanguageIndexInPath];
        }

        private static bool DoesURLPathContainOnlyOneSubdirectory(string[] pathSubDirectories)
        {
            return pathSubDirectories.Length < 2;
        }

        private static bool IsSecondPathInURLNullOrEmpty(string[] pathSubDirectories)
        {
            return string.IsNullOrEmpty(pathSubDirectories[LanguageIndexInPath]);
        }
    }
}

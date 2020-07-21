namespace CDNApplication.Utilities
{
    using Microsoft.AspNetCore.Http;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "Testing purposes")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Testing purposes")]
    public class SessionHelper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Testing purposes")]
        public static string GetLanguageFromContext(HttpContext context)
        {
            if (!context.Request.Path.HasValue)
            {
                return "en";
            }

            return GetLanguageFromPath(context.Request.Path.Value);
        }

        public static string GetLanguageFromPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "en";
            }

            var items = path.Split('/');
            if (items.Length < 2)
            {
                return "en";
            }

            if (string.IsNullOrEmpty(items[1]))
            {
                return "en";
            }

            return items[1];
        }
    }
}

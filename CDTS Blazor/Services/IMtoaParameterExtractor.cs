namespace CDNApplication.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the MTOA parameter extractor methods.
    /// </summary>
    public interface IMtoaParameterExtractor
    {
        /// <summary>
        /// Extracts the MTOA parameters from a given template.
        /// </summary>
        /// <param name="mtoaTemplate">The template to extract parameters from.</param>
        /// <returns>The list of parameters as a list of key value pairs.</returns>
        IEnumerable<KeyValuePair<string, string>> ExtractParameters(object mtoaTemplate);
    }
}

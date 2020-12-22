namespace CSF.Web.Client.Services
{
    using System;
    using System.Collections.Generic;
    using CSF.Web.Client.Data.Attributes;

    /// <summary>
    /// Defines the MTOA parameter extractor class.
    /// </summary>
    public class MtoaParameterExtractor : IMtoaParameterExtractor
    {
        /// <inheritdoc/>
        public IEnumerable<KeyValuePair<string, string>> ExtractParameters(object mtoaTemplate)
        {
            if (mtoaTemplate == null)
            {
                throw new ArgumentNullException(nameof(mtoaTemplate));
            }

            var parameters = new List<KeyValuePair<string, string>>();

            var templateClass = mtoaTemplate.GetType();
            var properties = templateClass.GetProperties();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    MtoaNotificationParameterNameAttribute mtoaNotificationParameterName = attribute as MtoaNotificationParameterNameAttribute;
                    if (mtoaNotificationParameterName != null)
                    {
                        var value = property.GetValue(mtoaTemplate);
                        if (value != null)
                        {
                            var key = mtoaNotificationParameterName.Name;
                            var keyValue = new KeyValuePair<string, string>(key, value.ToString());
                            parameters.Add(keyValue);
                        }
                    }
                }
            }

            return parameters;
        }
    }
}

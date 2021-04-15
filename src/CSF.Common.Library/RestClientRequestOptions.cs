using System;
using System.Collections.Generic;
using System.Text;

namespace CSF.Common.Library
{
    /// <summary>
    /// Defines options for the rest client.
    /// </summary>
    public class RestClientRequestOptions
    {
        /// <summary>
        /// Gets or sets the service name.
        /// </summary>
        public ServiceLocatorDomain ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the service path.
        /// </summary>
        public string Path { get; set; }


        /// <summary>
        /// Gets or sets the dataobject.
        /// </summary>
        public object DataObject { get; set; }

        /// <summary>
        /// Gets or sets the ParameterContentType
        /// </summary>
        public string ParameterContentType { get; set; }
    }
}

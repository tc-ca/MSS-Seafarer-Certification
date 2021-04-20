using CSF.Common.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CSF.Web.Client.Utilities.Entities
{
    /// <summary>
    /// Defines an object, which contains the rest client configuration.
    /// </summary>
    public class RestClientCallConfiguration
    {
        /// <summary>
        /// Gets or sets the service locator domain.
        /// </summary>
        public ServiceLocatorDomain ServiceLocatorDomain { get; set; }

        /// <summary>
        /// Gets or sets the path for the rest client call.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the data object.
        /// </summary>
        public object DataObject { get; set; }

        /// <summary>
        /// Gets or sets the content type header.
        /// </summary>
        public ContentTypeHeader ContentTypeHeader { get; set; }
    }
}

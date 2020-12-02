namespace CDNApplication.Utilities.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines a content type header.
    /// </summary>
    public enum ContentTypeHeader
    {
        /// <summary>
        /// Defines the header type application/json.
        /// </summary>
        [Description("application/json")]
        APPLICATIONJSON,

        /// <summary>
        /// Defines the header type application/octet-stream.
        /// </summary>
        [Description("application/octet-stream")]
        APPLICATIONOCTETSTREAM,
    }
}

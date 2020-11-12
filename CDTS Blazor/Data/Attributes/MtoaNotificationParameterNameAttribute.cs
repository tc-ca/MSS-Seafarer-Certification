namespace CDNApplication.Data.Attributes
{
    using System;

    /// <summary>
    /// Defines an attribute for the MTOA notification.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited=true, AllowMultiple = false)]
    public class MtoaNotificationParameterNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MtoaNotificationParameterNameAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        public MtoaNotificationParameterNameAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name attribute.
        /// </summary>
        public string Name { get; set; }
    }
}

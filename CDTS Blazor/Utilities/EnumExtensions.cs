namespace CDNApplication.Utilities
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// Defines the enum extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the string value of the the enum based off the [Description("")] attribute.
        /// </summary>
        /// <param name="value">The enum's value.</param>
        /// <returns>A string representation of the enum value.</returns>
        public static string GetValue(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(
                        field,
                        typeof(DescriptionAttribute)) as DescriptionAttribute;

                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }

            return null;
        }
    }
}
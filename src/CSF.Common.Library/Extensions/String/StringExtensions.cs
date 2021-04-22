namespace CSF.Common.Library.Extensions.String
{
    using System;
    public static class StringExtensions
    {
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct, IComparable
        {
            if(string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            value = value.Replace(" ", "_");

            T result;
            return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        }
    }
}

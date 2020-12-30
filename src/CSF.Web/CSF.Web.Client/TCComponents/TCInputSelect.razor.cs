namespace CSF.Web.Client.TCComponents
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;

    /// <summary>
    /// Defines the model for the select HTML element.
    /// </summary>
    /// <typeparam name="T">The type contained within the select HTML element.</typeparam>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "For Blazor in this case we need the class name to be different than the class name.")]
    public class TCInputSelectBase<T> : InputBase<T>
    {
        /// <summary>
        /// Gets or sets the Id for the HTML element.
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the label for the HTML element.
        /// </summary>
        [Parameter]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the validation for the select element.
        /// </summary>
        [Parameter]
        public Expression<Func<T>> ValidationFor { get; set; }

        /// <summary>
        /// Gets or sets the child content for the select element.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the default option.
        /// </summary>
        [Parameter]
        public bool ShowDefaultOption { get; set; } = true;

        /// <summary>
        /// Tries to partse the correct value from a string.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">The resulting parsed valued.</param>
        /// <param name="validationErrorMessage">The validation message to be used.</param>
        /// <returns>A boolean whether the parse was successful or not.</returns>
        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            if (typeof(T) == typeof(string))
            {
                result = (T)(object)value;
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T).IsEnum)
            {
                var success = BindConverter.TryConvertTo<T>(value, CultureInfo.CurrentCulture, out var parsedValue);
                if (success)
                {
                    result = parsedValue;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage = $"The {this.FieldIdentifier.FieldName} field is not valid.";
                    return false;
                }
            }

            throw new InvalidOperationException($"{this.GetType()} does not support the type '{typeof(T)}'.");
        }
    }
}

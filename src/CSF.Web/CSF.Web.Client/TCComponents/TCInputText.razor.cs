namespace CSF.Web.Client.TCComponents
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;

    /// <summary>
    /// Class for the model for the HTML text inputs.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "For Blazor in this case we need the class name to be different than the class name.")]
    public class TCInputTextBase : InputBase<string>
    {
        /// <summary>
        /// Gets or sets the Id for the HTML text input.
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the label for the HTML text input.
        /// </summary>
        [Parameter]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the TextInputType for generating the HTML.
        /// </summary>
        [Parameter]
        public TextInputType TextInputType { get; set; } = TextInputType.TEXT;

        /// <summary>
        /// Gets or sets the validation message for the Text input.
        /// </summary>
        [Parameter]
        public Expression<Func<string>> ValidationFor { get; set; }

        /// <summary>
        /// Converts the TextInputType to the HTML text type.
        /// </summary>
        /// <returns>The string representing the HTML text type.</returns>
        public string TextInputTypeString()
        {
            switch (this.TextInputType)
            {
                case TextInputType.DATE:
                    return "date";
                case TextInputType.DATETIMELOCAL:
                    return "datetime-local";
                case TextInputType.EMAIL:
                    return "email";
                case TextInputType.MONTH:
                    return "month";
                case TextInputType.NUMBER:
                    return "number";
                case TextInputType.PASSWORD:
                    return "password";
                case TextInputType.SEARCH:
                    return "search";
                case TextInputType.TEL:
                    return "tel";
                case TextInputType.TIME:
                    return "time";
                case TextInputType.URL:
                    return "url";
                case TextInputType.WEEK:
                    return "week";
                case TextInputType.TEXT:
                default:
                    return "text";
            }
        }

        /// <summary>
        /// Try to parse the value from the string.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">The resulting string.</param>
        /// <param name="validationErrorMessage">The validation error message.</param>
        /// <returns>Whether the parse value was successful.</returns>
        protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}

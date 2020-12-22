namespace CSF.Web.Client.TCComponents
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;

    /// <summary>
    /// Custom class for radio buttons.
    /// </summary>
    /// <typeparam name="T">The type to be used for the radio button.</typeparam>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "For Blazor in this case we need the class name to be different than the class name.")]
    public class TCInputRadioBase<T> : InputBase<T>
    {
        /// <summary>
        /// Gets or sets the radio buttons value.
        /// </summary>
        [Parameter]
        public T SelectedValue { get; set; }

        /// <summary>
        /// Gets or sets the radio buttons HTMNL Id.
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the rafio buttons HTML Label.
        /// </summary>
        [Parameter]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the validation for the radio button.
        /// </summary>
        [Parameter]
        public Expression<Func<T>> ValidationFor { get; set; }

        /// <summary>
        /// Defines what happens when the state of the radio button changes.
        /// </summary>
        /// <param name="args">The OnChangeEvent arguments.</param>
        protected void OnChange(ChangeEventArgs args)
        {
            if (args != null)
            {
                this.CurrentValueAsString = args.Value.ToString();
            }
        }

        /// <inheritdoc/>
        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
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
                validationErrorMessage = $"{this.FieldIdentifier.FieldName} field isn't valid.";
                return false;
            }
        }
    }
}

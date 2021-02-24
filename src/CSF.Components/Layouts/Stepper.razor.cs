namespace CSF.Components.Layouts
{
    using System;
    using Microsoft.AspNetCore.Components;

    public partial class Stepper
    {
        /// <summary>
        /// Gets or sets the Header render fragment of the Stepper.
        /// </summary>
        [Parameter]
        public RenderFragment Header { get; set; }

        /// <summary>
        /// Gets or sets the Body render fragment of the Stepper.
        /// </summary>
        [Parameter]
        public RenderFragment Body { get; set; }

        /// <summary>
        /// Gets or sets the parent of the stepper.
        /// </summary>
        [CascadingParameter]
        public StepperContainer Parent { get; set; }

        public string CssClass { 
            get
            {
                string cssClass = "step";
                cssClass += !this.IsActive ? " minimized" : "";
                cssClass += this.IsComplete ? " complete" : "";
                return cssClass;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current stepper is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current stepper is complete.
        /// </summary>
        public bool IsComplete { get; set; }

        protected override void OnInitialized()
        {
            if (this.Parent == null)
                throw new ArgumentNullException(nameof(this.Parent), "Stepper must exist within a StepperContainer");
            base.OnInitialized();
            this.Parent.AddStep(this);
        }
    }
}

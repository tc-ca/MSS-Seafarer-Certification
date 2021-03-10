namespace CSF.Components.Layouts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Components;
    public partial class StepperContainer
    {
        private int CurrentActiveStepIndex;

        [Parameter]
        public string ButtonText { get; set; }

        [Parameter]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the ChildContenet of the StepperContainer.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        /// <summary>
        /// Gets or sets the list of steppers.
        /// </summary>
        public List<Stepper> Steppers { get; set; } = new List<Stepper>();

        /// <summary>
        /// Adds a Stepper to the StepperContainer.
        /// </summary>
        /// <param name="stepper"></param>
        public void AddStep(Stepper stepper)
        {
            this.Steppers.Add(stepper);

            if (this.Steppers.Count == 1)
            {
                this.CurrentActiveStepIndex = 0;
                stepper.IsActive = true;
            }
        }

        /// <summary>
        /// Steps to the next step in the StepperContainer.
        /// </summary>
        public void NextStep()
        {
            if (this.Steppers.Count > this.CurrentActiveStepIndex + 1)
            {
                this.Steppers[CurrentActiveStepIndex].IsActive = false;
                this.Steppers[CurrentActiveStepIndex].IsComplete = true;
                this.Steppers[CurrentActiveStepIndex + 1].IsActive = true;
                this.CurrentActiveStepIndex++;
            }
        }

        /// <summary>
        /// Steps to the previous step in the StepperContainer.
        /// </summary>
        public void PreviousStep()
        {
            if ((this.CurrentActiveStepIndex - 1) >= 0)
            {
                this.Steppers[CurrentActiveStepIndex].IsActive = false;
                this.Steppers[CurrentActiveStepIndex - 1].IsActive = true;
                this.Steppers[CurrentActiveStepIndex - 1].IsComplete = false;
                this.CurrentActiveStepIndex--;
            }
        }
    }
}

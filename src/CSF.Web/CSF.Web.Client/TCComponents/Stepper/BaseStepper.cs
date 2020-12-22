namespace CSF.Web.Client.TCComponents.Stepper
{
    using System;
    using System.Collections.Generic;

    public class BaseStepper : IStepper
    {
        private List<IStep> steps;

        public int ActiveIndex { get; set; }

        public int AmountOfSteps
        {
            get
            {
                return this.steps.Count;
            }
        }

        public BaseStepper()
        {
            this.steps = new List<IStep>();
        }

        public void ActivateStepAtIndex(int index)
        {
            var currentStep = this.GetStepAtIndex(index);
            for (int i = 0; i < index; i++)
            {
                var previousStep = this.GetStepAtIndex(i);
                this.setStepAsComplete(previousStep);
            }
            for (int i = index; i < this.AmountOfSteps - 1; i++)
            {
                var nextStep = this.GetStepAtIndex(i);
                this.setStepAsInactive(nextStep);
            }
            this.setStepAsActive(currentStep);
        }

        private void setStepAsComplete(IStep step)
        {
            step.IsActive = false;
            step.IsComplete = true;
        }

        private void setStepAsInactive(IStep step)
        {
            step.IsActive = false;
            step.IsComplete = false;
        }

        private void setStepAsActive(IStep step)
        {
            step.IsActive = true;
            step.IsComplete = false;
        }

        public IStep GetStepAtIndex(int index)
        {
            return this.steps[index];
        }

        public void AddStep(IStep step)
        {
            this.steps.Add(step);
        }
    }
}

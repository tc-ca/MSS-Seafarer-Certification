namespace CDNApplication.TCComponents.Stepper
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

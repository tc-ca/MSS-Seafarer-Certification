using System;
namespace CSF.Web.Client.TCComponents.Stepper
{
    public interface IStepper
    {
        public int ActiveIndex { get; set; }
        public int AmountOfSteps { get; }

        IStep GetStepAtIndex(int index);
        void AddStep(IStep step);
    }
}

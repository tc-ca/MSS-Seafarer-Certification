using System;
namespace CSF.Web.Client.TCComponents.Stepper
{
    public interface IStep
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
        public string cssClass { get; }
    }
}

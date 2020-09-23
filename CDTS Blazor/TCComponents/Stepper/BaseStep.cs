using System;
namespace CDNApplication.TCComponents.Stepper
{
    public class BaseStep : IStep
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
        public string cssClass
        {
            get
            {
                string cssClass = string.Empty;
                cssClass += this.IsComplete ? " complete" : "";
                cssClass += this.IsActive ? " active" : "";
                return cssClass;
            }
        }
    }
}

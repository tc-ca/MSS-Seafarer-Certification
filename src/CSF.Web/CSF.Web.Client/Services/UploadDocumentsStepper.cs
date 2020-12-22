using System;
using CSF.Web.Client.Shared;
using CSF.Web.Client.TCComponents.Stepper;
using Microsoft.Extensions.Localization;

namespace CSF.Web.Client.Services
{
    public class UploadDocumentsStepper
    {
        private int _currentIndex = 0;

        public BaseStepper Stepper { get; set; }
        public IStringLocalizer<Common> CommonLocalizer { get; set; }
        
        public UploadDocumentsStepper(IStringLocalizer<Common> CommonLocalizer)
        {
            this.CommonLocalizer = CommonLocalizer;
            this.Stepper = new BaseStepper();
            this.initStepper();
        }

        private void initStepper()
        {
            var step1 = new CSF.Web.Client.TCComponents.Stepper.BaseStep
            {
                Text = CommonLocalizer["DocumentUpload_Step1"],
                Index = 0,
                IsComplete = false,
                IsActive = true
            };
            var step2 = new CSF.Web.Client.TCComponents.Stepper.BaseStep
            {
                Text = CommonLocalizer["DocumentUpload_Step2"],
                Index = 1,
                IsComplete = false,
                IsActive = false
            };
            var step3 = new CSF.Web.Client.TCComponents.Stepper.BaseStep
            {
                Text = CommonLocalizer["DocumentUpload_Step3"],
                Index = 2,
                IsComplete = false,
                IsActive = false
            };

            this.Stepper.AddStep(step1);
            this.Stepper.AddStep(step2);
            this.Stepper.AddStep(step3);
        }
    }
}

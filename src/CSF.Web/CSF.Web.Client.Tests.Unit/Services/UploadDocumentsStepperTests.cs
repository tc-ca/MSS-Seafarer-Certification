namespace CDNApplication.Tests.Unit.Services
{
    using CDNApplication.Services;
    using CDNApplication.Shared;
    using Microsoft.Extensions.Localization;
    using Moq;
    using Xunit;

    public class UploadDocumentsStepperTests
    {
        [Fact]
        public void initStepper_ConstructsProperly()
        {
            // Arrange
            var stringLocalizer = new Mock<IStringLocalizer<Common>>();
            stringLocalizer.Setup(localizer => localizer["DocumentUpload_Step1"]).Returns(new LocalizedString("DocumentUpload_Step1", "Step 1"));
            stringLocalizer.Setup(localizer => localizer["DocumentUpload_Step2"]).Returns(new LocalizedString("DocumentUpload_Step2", "Step 2"));
            stringLocalizer.Setup(localizer => localizer["DocumentUpload_Step3"]).Returns(new LocalizedString("DocumentUpload_Step3", "Step 3"));

            var expectedStep1Text = "Step 1";
            var expectedStep1Index = 0;
            var expectedStep1IsComplete = false;
            var expectedStep1IsActive = true;

            var expectedStep2Text = "Step 2";
            var expectedStep2Index = 1;
            var expectedStep2IsComplete = false;
            var expectedStep2IsActive = false;

            var expectedStep3Text = "Step 3";
            var expectedStep3Index = 2;
            var expectedStep3IsComplete = false;
            var expectedStep3IsActive = false;

            // Act
            var uploadDocumentsStepper = new UploadDocumentsStepper(stringLocalizer.Object);
            var step1 = uploadDocumentsStepper.Stepper.GetStepAtIndex(0);
            var step2 = uploadDocumentsStepper.Stepper.GetStepAtIndex(1);
            var step3 = uploadDocumentsStepper.Stepper.GetStepAtIndex(2);

            // Assert
            Assert.Equal(step1.Text, expectedStep1Text);
            Assert.Equal(step1.Index, expectedStep1Index);
            Assert.Equal(step1.IsComplete, expectedStep1IsComplete);
            Assert.Equal(step1.IsActive, expectedStep1IsActive);

            Assert.Equal(step2.Text, expectedStep2Text);
            Assert.Equal(step2.Index, expectedStep2Index);
            Assert.Equal(step2.IsComplete, expectedStep2IsComplete);
            Assert.Equal(step2.IsActive, expectedStep2IsActive);

            Assert.Equal(step3.Text, expectedStep3Text);
            Assert.Equal(step3.Index, expectedStep3Index);
            Assert.Equal(step3.IsComplete, expectedStep3IsComplete);
            Assert.Equal(step3.IsActive, expectedStep3IsActive);
        }
    }
}

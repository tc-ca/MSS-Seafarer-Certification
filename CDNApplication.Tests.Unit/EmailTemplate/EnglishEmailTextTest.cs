namespace CDNApplication.Tests.Unit.EmailTemplate
{
    using System.IO;
    using Xunit;

    public class EnglishEmailTextTest
    {
        [Fact]
        public void EnglishIntroductionText_IsValid()
        {
            // Arrange
            var confirmationNmuber = "1234567";
            var exptectedIntroductionText = this.GetIntroductionText(confirmationNmuber);

            // Act
            var result = string.Format(File.ReadAllText($"Resources/EmailTemplates/Parameters/SubmissionEmailTemplateIntroductionEnglish.html").Replace(System.Environment.NewLine, ""), confirmationNmuber);

            // Assert
            Assert.Equal(exptectedIntroductionText, result);
        }

        [Fact]
        public void EnglishSignatureText_IsValid()
        {
            // Arrange
            var exptectedIntroductionText = this.GetSignatureText();

            // Act
            var result = File.ReadAllText($"Resources/EmailTemplates/Parameters/SubmissionEmailTemplateSignatureEnglish.html").Replace(System.Environment.NewLine, "");

            // Assert
            Assert.Equal(exptectedIntroductionText, result);
        }

        private string GetIntroductionText(string confirmationNumber)
        {
            return string.Format(@"*This is a system generated message. Please do not reply.*</i></p><p>Hello,<br/><p>We have received your documents for your seafarer certification application.</p><h4>Your confirmation number is {0}.</h4><p>Please save your confirmation number in a safe place. You will need this number if you contact us about your submission.</p>", confirmationNumber);
        }

        private string GetSignatureText()
        {
            return @"<h4>What happens next?</h4><p>Our team will review your documents within the next 10 working days. We will ensure they meet the requirements for your selected certificate.</p><p>If you meet the requirements, you can expect to receive your certificate:</p><ul><li>Within 10 working days for an Examiner's Certificate</li><li>Within 120 working days for a Minister's Certificate</li></ul><p>We will contact you by the phone number or email address your provided if we need any more information.</p><p>If you have questions or concerns, contact your local <a href=""https://tc.canada.ca/en/corporate-services/regions"" target=""_blank"">Transport Canada Service Centre</a>. Be sure to have your confirmation number ready to share.</p><p>Thank you,</p><div class=""sig"">Marine Personnel Certification<br>Transport Canada, Government of Canada<br><a href=""mailto:TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca"">TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca</a> / 1-855-859-3123 (5 for Seafarer Services)</div>";
        }
    }
}

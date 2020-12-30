namespace CSF.Web.Client.Tests.Unit.EmailTemplate
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

        [Fact]
        public void EnglishSubjectTitleText_IsValid()
        {
            // Arrange
            var expectedSubjectTitleText = this.GetSubjectTitleText();

            // Act
            var result = File.ReadAllText($"Resources/EmailTemplates/SubmissionEmailTemplateSubjectTextEnglish.html").Replace(System.Environment.NewLine, "");

            // Assert
            Assert.Equal(expectedSubjectTitleText, result);
        }

        [Fact]
        public void EnglishBodyText_IsValid()
        {
            // Arrange
            var expectedBodyText = this.GetBodyText();

            // Act
            var result = File.ReadAllText($"Resources/EmailTemplates/SubmissionEmailTemplateBodyTextEnglish.html").Replace(System.Environment.NewLine, "");

            // Assert
            Assert.Equal(expectedBodyText, result);
        }

        private string GetIntroductionText(string confirmationNumber)
        {
            return string.Format(@"*This is a system generated message. Please do not reply.*</i></p><p>Hello,<br/><p>We have received your documents for your seafarer certification application.</p><h4>Your confirmation number is {0}.</h4><p>Please save your confirmation number in a safe place. You will need this number if you contact us about your submission.</p>", confirmationNumber);
        }

        private string GetSignatureText()
        {
            return @"<h4>What happens next?</h4><p>Our team will review your documents within the next 10 working days. We will ensure they meet the requirements for your selected certificate.</p><p>If you meet the requirements, you can expect to receive your certificate:</p><ul><li>Within 10 working days for an Examiner's Certificate</li><li>Within 120 working days for a Minister's Certificate</li></ul><p>We will contact you by the phone number or email address your provided if we need any more information.</p><p>If you have questions or concerns, contact your local <a href=""https://tc.canada.ca/en/corporate-services/regions"" target=""_blank"">Transport Canada Service Centre</a>. Be sure to have your confirmation number ready to share.</p><p>Thank you,</p><div class=""sig"">Marine Personnel Certification<br>Transport Canada, Government of Canada<br><a href=""mailto:TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca"">TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca</a> / 1-855-859-3123 (5 for Seafarer Services)</div>";
        }

        private string GetSubjectTitleText()
        {
            return "Transport Canada has received your documents / Transports Canada a reçu vos documents - Confirmation # {Confirmation_Number}";
        }

        private string GetBodyText()
        {
            return @"<head><style>body { font-family: Arial; line-height: 25px; }table, th, td { font-family: Arial; border: 1px solid black; border-collapse: collapse; padding: 10px; }.sig{ font-size: small; }</style></head><body><p><i>**Le texte français suit l’anglais**<br />{English_Introduction}<h4>Here is a record of your submission:</h4><table><tr><td>Candidate Document Number</td><td>{CDN_Number}</td></tr><tr><td>Phone number</td><td>{Phone_Number}</td></tr><tr><td>Email address</td><td>{Email_Address}</td></tr><tr><td>Selected certificate</td><td>{Selected_CertificateType}</td></tr><tr><td>Type of certificate</td><td>{Submission_Type_En}</td></tr><tr><td>Documents</td><td>{DOCUMENT}</td></tr></table>{English_Signature}<hr /><p><i>{French_Introduction}<h4>Voici un relevé de votre demande:</h4><table><tr><td>Numéro de candidat</td><td>{CDN_Number}</td></tr><tr><td>Numéro de téléphone</td><td>{Phone_Number}</td></tr><tr><td>Adresse électronique</td><td>{Email_Address}</td></tr><tr><td>Brevet sélectionné</td><td>{Selected_CertificateType}</td></tr><tr><td>Type de brevet</td><td>{Submission_Type_Fr}</td></tr><tr><td>Documents</td><td>{DOCUMENT}</td></tr></table>{French_Signature}</body>";
        }
    }
}

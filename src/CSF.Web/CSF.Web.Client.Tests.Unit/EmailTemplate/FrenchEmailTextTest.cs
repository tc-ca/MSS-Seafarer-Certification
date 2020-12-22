namespace CSF.Web.Client.Tests.Unit.EmailTemplate
{
    using System.IO;
    using Xunit;

    public class FrenchEmailTextTest
    {
        [Fact]
        public void FrenchIntroductionText_IsValid()
        {
            // Arrange
            var confirmationNmuber = "1234567";
            var exptectedIntroductionText = this.GetIntroductionText(confirmationNmuber);

            // Act
            var result = string.Format(File.ReadAllText($"Resources/EmailTemplates/Parameters/SubmissionEmailTemplateIntroductionFrench.html").Replace(System.Environment.NewLine, ""), confirmationNmuber);

            // Assert
            Assert.Equal(exptectedIntroductionText, result);
        }

        [Fact]
        public void FrenchSignatureText_IsValid()
        {
            // Arrange
            var exptectedIntroductionText = this.GetSignatureText();

            // Act
            var result = File.ReadAllText($"Resources/EmailTemplates/Parameters/SubmissionEmailTemplateSignatureFrench.html").Replace(System.Environment.NewLine, "");

            // Assert
            Assert.Equal(exptectedIntroductionText, result);
        }

        [Fact]
        public void FrenchSubjectTitleText_IsValid()
        {
            // Arrange
            var expectedSubjectTitleText = this.GetSubjectTitleText();

            // Act
            var result = File.ReadAllText($"Resources/EmailTemplates/SubmissionEmailTemplateSubjectTextFrench.html").Replace(System.Environment.NewLine, "");

            // Assert
            Assert.Equal(expectedSubjectTitleText, result);
        }

        [Fact]
        public void FrenchBodyText_IsValid()
        {
            // Arrange
            var expectedBodyText = this.GetBodyText();

            // Act
            var result = File.ReadAllText($"Resources/EmailTemplates/SubmissionEmailTemplateBodyTextFrench.html").Replace(System.Environment.NewLine, "");

            // Assert
            Assert.Equal(expectedBodyText, result);
        }

        private string GetIntroductionText(string confirmationNumber)
        {
            return string.Format(@"*Il s’agit d’un message généré par le système. Veuillez ne pas y répondre.*</i></p><p>Bonjour,</p><p>Nous avons reçu vos documents pour votre demande de brevet délivré aux gens de mer.</p><h4>Votre numéro de confirmation est le {0}.</h4><p>Veuillez prendre en note votre numéro de confirmation. Vous aurez besoin de ce numéro si vous communiquez avec nous au sujet de votre demande.</p>", confirmationNumber);
        }

        private string GetSignatureText()
        {
            return @"<h4>Quelles sont les prochaines étapes?</h4><p>Notre équipe examinera vos documents dans un délai de 10 jours ouvrables. Nous nous assurerons qu’ils répondent aux exigences du brevet que vous avez sélectionné.</p><p>Si vous remplissez les conditions requises, vous pouvez vous attendre à recevoir votre brevet:</p><ul><li>Dans un délai de 10 jours ouvrables pour brevet de l’examinateur</li><li>Dans un délai de 120 jours ouvrables pour un brevet du ministre</li></ul><p>Nous communiquerons avec vous au moyen du numéro de téléphone ou de l’adresse électronique que vous nous avez fournis, si nous avons besoin de plus de renseignements.</p><p>Si vous avez des questions ou des préoccupations, communiquez avec le <a href=""https://tc.canada.ca/fr/services-generaux/regions"" target=""_blank"">Centre de services de Transports Canada</a> de votre région. Assurez-vous d’avoir votre numéro de confirmation à portée de main.</p><p>Merci.</p><div class=""sig"">Certification du personnel maritime<br>Transports Canada, Gouvernement du Canada<br><a href=""mailto:TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca"">TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca</a> / 1-855-859-3123 (5 pour services aux gens de mer)</div>";
        }

        private string GetSubjectTitleText()
        {
            return "Transports Canada a reçu vos documents / Transport Canada has received your documents - Confirmation # {Confirmation_Number}";
        }

        private string GetBodyText()
        {
            return @"<head><style>body { font-family: Arial; line-height: 25px; }table, th, td { font-family: Arial; border: 1px solid black; border-collapse: collapse; padding: 10px; }.sig{ font-size: small; }</style></head><body><p><i>**English text follows French**<br/>{French_Introduction}<h4>Voici un relevé de votre demande:</h4><table><tr><td>Numéro de candidat</td><td>{CDN_Number}</td></tr><tr><td>Numéro de téléphone</td><td>{Phone_Number}</td></tr><tr><td>Adresse électronique</td><td>{Email_Address}</td></tr><tr><td>Brevet sélectionné</td><td>{Selected_CertificateType}</td></tr><tr><td>Type de brevet</td><td>{Submission_Type_Fr}</td></tr><tr><td>Documents</td><td>{DOCUMENT}</td></tr></table>{French_Signature}<hr /><p><i>{English_Introduction}<h4>Here is a record of your submission:</h4><table><tr><td>Candidate Document Number</td><td>{CDN_Number}</td></tr><tr><td>Phone number</td><td>{Phone_Number}</td></tr><tr><td>Email address</td><td>{Email_Address}</td></tr><tr><td>Selected certificate</td><td>{Selected_CertificateType}</td></tr><tr><td>Type of certificate</td><td>{Submission_Type_En}</td></tr><tr><td>Documents</td><td>{DOCUMENT}</td></tr></table>{English_Signature}</body>";
        }
    }
}

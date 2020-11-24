﻿namespace CDNApplication.Tests.Unit.EmailTemplate
{
    using System.IO;
    using Xunit;

    public class FrenchSignatureTextTest
    {
        [Fact]
        public void FrenchSignatureText_IsValid()
        {
            // Arrange
            var confirmationNmuber = "1234567";
            var exptectedIntroductionText = this.GetSignatureText();

            // Act
            var result = string.Format(File.ReadAllText($"Resources/EmailTemplates/Parameters/SubmissionEmailTemplateSignatureFrench.html").Replace(System.Environment.NewLine, ""), confirmationNmuber);

            // Assert
            Assert.Equal(exptectedIntroductionText, result);
        }

        private string GetSignatureText()
        {
            return @"<h4>Quelles sont les prochaines étapes?</h4><p>Notre équipe examinera vos documents dans un délai de 10 jours ouvrables. Nous nous assurerons qu’ils répondent aux exigences du brevet que vous avez sélectionné.</p<p>Si vous remplissez les conditions requises, vous pouvez vous attendre à recevoir votre brevet:</p><ul><li>Dans un délai de 10 jours ouvrables pour brevet de l’examinateur</li><li>Dans un délai de 120 jours ouvrables pour un brevet du ministre</li></ul><p>Nous communiquerons avec vous au moyen du numéro de téléphone ou de l’adresse électronique que vous nous avez fournis, si nous avons besoin de plus de renseignements.</p><p>Si vous avez des questions ou des préoccupations, communiquez avec le <a href=""https://tc.canada.ca/fr/services-generaux/regions"" target=""_blank"">Centre de services de Transports Canada</a> de votre région. Assurez-vous d’avoir votre numéro de confirmation à portée de main.</p><p>Merci.</p><div class=""sig"">Certification du personnel maritime<br>Transports Canada, Gouvernement du Canada<br><a href=""mailto:TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca"">TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca</a> / 1-855-859-3123 (5 pour services aux gens de mer)</div>";
        }
    }
}


/*
@"<h4>Quelles sont les prochaines étapes?</h4><p>Notre équipe examinera vos documents dans un délai de 10 jours ouvrables. Nous nous assurerons qu’ils répondent aux exigences du brevet que vous avez sélectionné.</p<p>Si vous remplissez les conditions requises, vous pouvez vous attendre à recevoir votre brevet:</p><ul><li>Dans un délai de 10 jours ouvrables pour brevet de l’examinateur</li><li>Dans un délai de 120 jours ouvrables pour un brevet du ministre</li></ul><p>Nous communiquerons avec vous au moyen du numéro de téléphone ou de l’adresse électronique que vous nous avez fournis, si nous avons besoin de plus de renseignements.</p><p>Si vous avez des questions ou des préoccupations, communiquez avec le <a href="https://tc.canada.ca/fr/services-generaux/regions" target="_blank">Centre de services de Transports Canada</a> de votre région. Assurez-vous d’avoir votre numéro de confirmation à portée de main.</p><p>Merci.</p><div class="sig">Certification du personnel maritime<br>Transports Canada, Gouvernement du Canada<br><a href="mailto:TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca">TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca</a> / 1-855-859-3123 (5 pour services aux gens de mer)</div>";
*/
namespace CDNApplication.Tests.Unit.EmailTemplate
{
    using System.IO;
    using Xunit;

    public class FrenchIntroductionTextTest
    {
        [Fact]
        public void FrenchIntroduction_Text_IsValid()
        {
            // Arrange
            var confirmationNmuber = "1234567";
            var exptectedIntroductionText = this.GetIntroductionText(confirmationNmuber);

            // Act
            var result = string.Format(File.ReadAllText($"Resources/EmailTemplates/Parameters/SubmissionEmailTemplateIntroductionFrench.html").Replace(System.Environment.NewLine, ""), confirmationNmuber);

            // Assert
            Assert.Equal(exptectedIntroductionText, result);
        }

        private string GetIntroductionText(string confirmationNumber)
        {
            return string.Format(@"*Il s’agit d’un message généré par le système. Veuillez ne pas y répondre.*</i></p><p>Bonjour,</p><p>Nous avons reçu vos documents pour votre demande de brevet délivré aux gens de mer.</p><h4>Votre numéro de confirmation est le {0}.</h4><p>Veuillez prendre en note votre numéro de confirmation. Vous aurez besoin de ce numéro si vous communiquez avec nous au sujet de votre demande.</p>", confirmationNumber);
        }
    }
}

namespace CDNApplication.Data.DTO.MTAPI
{
    using CDNApplication.Data.Attributes;

    /// <summary>
    /// Defines the MTOA seafarer submission email parameters.
    /// This class is primarily used to workaround the 4000 character limit from MTOA.
    /// If this changes in the future we want to remove this comment and this class.
    /// </summary>
    public class MtoaSeafarersSubmissionEmailParametersDto : MtoaSeafarersDocumentSubmissionEmailParametersDto
    {
        /// <summary>
        /// Gets or sets the English introduction HTML.
        /// </summary>
        [MtoaNotificationParameterName("English_Introduction")]
        public string EnglishIntroduction { get; set; }

        /// <summary>
        /// Gets or sets the English signature HTML.
        /// </summary>
        [MtoaNotificationParameterName("English_Signature")]
        public string EnglishSignature { get; set; }

        /// <summary>
        /// Gets or sets the French introduction HTML.
        /// </summary>
        [MtoaNotificationParameterName("French_Introduction")]
        public string FrenchIntroduction { get; set; }

        /// <summary>
        /// Gets or sets the French signature HTML.
        /// </summary>
        [MtoaNotificationParameterName("French_Signature")]
        public string FrenchSignature { get; set; }
    }
}

/*

<head>
   <style>body {
      font-family: Arial; line-height: 25px;
      }
      table, th, td {
      font-family: Arial; border: 1px solid black; border-collapse: collapse; padding: 10px; }
      .sig{
      font-size: small; }
   </style>
</head>
<body>
   {Introduction_English}
   <h4>Here is a record of your submission:</h4>
   <table>
      <tr>
         <td>Candidate Document Number</td>
         <td>{CDN_Number}</td>
      </tr>
      <tr>
         <td>Phone number</td>
         <td>{Phone_Number}</td>
      </tr>
      <tr>
         <td>Email address</td>
         <td>{Email_Address}</td>
      </tr>
      <tr>
         <td>Selected certificate</td>
         <td>{Selected_CertificateType}</td>
      </tr>
      <tr>
         <td>Documents</td>
         <td>{DOCUMENT}</td>
      </tr>
   </table>
   {Signature_English}

   <hr>

   {Introduction_French}
   <h4>Voici un relevé de votre demande:</h4>
   <table>
      <tr>
         <td>Numéro de candidat</td>
         <td>{CDN_Number}</td>
      </tr>
      <tr>
         <td>Numéro de téléphone</td>
         <td>{Phone_Number}</td>
      </tr>
      <tr>
         <td>Adresse électronique</td>
         <td>{Email_Address}</td>
      </tr>
      <tr>
         <td>Brevet sélectionné</td>
         <td>{Selected_CertificateType}</td>
      </tr>
      <tr>
         <td>Documents</td>
         <td>{DOCUMENT}</td>
      </tr>
   </table>
   {Signature_French}
</body>
 
*/
/*
		{Introduction_English}
	        <p><i>*Le texte français suit l’anglais*<br>*This is a system generated message. Please do not reply.*</i></p>
           <p>Hello,</br>
           <p>We have received your documents for your seafarer certification application.</p>
           <h4>Your confirmation number is {Confirmation_Number}.</h4>
           <p>Please save your confirmation number in a safe place. You will need this number if you contact us about your submission.</p>
        {Signature_English}
           <h4>What happens next?</h4>
           <p>Our team will review your documents within the next 10 working days. We will ensure they meet the requirements for your selected certificate.</p>
           <p>If you meet the requirements, you can expect to receive your certificate:</p>
           <ul>
              <li>Within 10 working days for an Examiner's Certificate</li>
              <li>Within 120 working days for a Minister's Certificate</li>
           </ul>
           <p>We will contact you by the phone number or email address your provided if we need any more information.</p>
           <p>If you have questions or concerns, contact your local <a href="https://tc.canada.ca/en/corporate-services/regions" target="_blank">Transport Canada Service Centre</a>. Be sure to have your confirmation number ready to share.</p>
           <p>Thank you,</p>
           <div class="sig">Marine Personnel Certification<br>Transport Canada, Government of Canada<br><a href="mailto:TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca">TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca</a> / 1-855-859-3123 (5 for Seafarer Services)</div>
        
        {Introduction_French}
           <p><i>*Il s’agit d’un message généré par le système. Veuillez ne pas y répondre.*</i></p>
           <p>Bonjour,</p>
           <p>Nous avons reçu vos documents pour votre demande de brevet délivré aux gens de mer.</p>
           <h4>Votre numéro de confirmation est le {Confirmation_Number}.</h4>
           <p>Veuillez prendre en note votre numéro de confirmation. Vous aurez besoin de ce numéro si vous communiquez avec nous au sujet de votre demande.</p>

        {Signature_French}
           <h4>Quelles sont les prochaines étapes?</h4>
           <p>Notre équipe examinera vos documents dans un délai de 10 jours ouvrables. Nous nous assurerons qu’ils répondent aux exigences du brevet que vous avez sélectionné.</p>
           <p>Si vous remplissez les conditions requises, vous pouvez vous attendre à recevoir votre brevet:</p>
           <ul>
              <li>Dans un délai de 10 jours ouvrables pour brevet de l’examinateur</li>
              <li>Dans un délai de 120 jours ouvrables pour un brevet du ministre</li>
           </ul>
           <p>Nous communiquerons avec vous au moyen du numéro de téléphone ou de l’adresse électronique que vous nous avez fournis, si nous avons besoin de plus de renseignements.</p>
           <p>Si vous avez des questions ou des préoccupations, communiquez avec le <a href="https://tc.canada.ca/fr/services-generaux/regions" target="_blank">Centre de services de Transports Canada</a> de votre région. Assurez-vous d’avoir votre numéro de confirmation à portée de main.</p>
           <p>Merci.</p>
           <div class="sig">Certification du personnel maritime<br>Transports Canada, Gouvernement du Canada<br><a href="mailto:TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca">TC.MarinePersonnelIssues-Questionspersonnelmaritime.TC@tc.gc.ca</a> / 1-855-859-3123 (5 pour services aux gens de mer)</div>
*/
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
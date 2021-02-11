namespace CSF.API.Data.Entities
{
    /// <summary>
    /// Defines a CertificateType
    /// </summary>
    public class CertificateType
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets English Name.
        /// </summary> 
        public string EnglishName { get; set; }

        /// <summary>
        /// Gets or sets French Name.
        /// </summary>
        public string FrenchName { get; set; }
    }
}

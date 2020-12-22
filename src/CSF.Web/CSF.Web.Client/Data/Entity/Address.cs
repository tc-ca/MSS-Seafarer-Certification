namespace CSF.Web.Client.Data.Entity
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The applicant's main living address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the Appartment Unit Number.
        /// </summary>
        public string ApartmentUnitNumber { get; set; }

        /// <summary>
        /// Gets or sets the street number.
        /// </summary>
        [Required]
        public string StreetNumber { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        [Required]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the PO Box.
        /// </summary>
        public string POBox { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        [Required]
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the Postal code.
        /// </summary>
        [Required]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the telphone number.
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
    }
}

using CSF.Web.Client.Data.DTO.MPDIS;

namespace CSF.Web.Client.Services.MPDIS
{
    /// <summary>
    /// Defines the interface for the MPDIS service.
    /// </summary>
    public interface IMpdisService
    {
        /// <summary>
        /// Gets the applicant's information from the cdn.
        /// </summary>
        /// <param name="cdn">The applicant's cdn.</param>
        /// <returns>The applicant's information.</returns>
        ApplicantInformationDto GetApplicantByCdn(string cdn);
    }
}
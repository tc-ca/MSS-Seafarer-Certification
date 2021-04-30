using MPDIS.API.Wrapper.Services.MPDIS.Entities;

namespace MPDIS.API.Wrapper.Services.MPDIS
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
        ApplicantInformation GetApplicantByCdn(string cdn);

        /// <summary>
        /// Search for the applicant based on CDN number, First Name, Last Name, or date of birth
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>Results based on search criteria</returns>
        ApplicantSearchResult Search(ApplicantSearchCriteria searchCriteria);

        /// <summary>
        /// Gets the applicant's personal info without medical assessmentinfo from applicant-infomration
        /// </summary>
        /// <param name="applicantInfo">ApplicantInfomration object that is received from MPDIS call</param>
        /// <returns>The applicant's personal info without medical assessmentinfo </returns>
        ApplicantPersonalInfo GetPersonalInfoFromApplicantInfo(ApplicantInformation applicantInfo);
    }
}

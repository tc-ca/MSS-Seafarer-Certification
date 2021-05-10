using System;
using System.Net.Http;
using CSF.Common.Library;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MPDIS.API.Wrapper.Services.MPDIS
{
    /// <summary>
    /// The Mpdis service.
    /// </summary>
    public class MpdisService : IMpdisService
    {
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly ILogger<MpdisService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MpdisService"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration service.</param>
        /// <param name="restClient">REST client service.</param>
        /// <param name="logger">Application logger service.</param>
        public MpdisService(IConfiguration configuration, IRestClient restClient, ILogger<MpdisService> logger)
        {
            this.configuration = configuration;
            this.restClient = restClient;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public FullApplicantInformation GetApplicantByCdn(string cdn)
        {
            var serviceRequestPath = string.Format("applicants/cdn/{0}", cdn);
            try
            {
                return this.restClient.GetAsync<FullApplicantInformation>(ServiceLocatorDomain.Mpdis, serviceRequestPath).GetAwaiter().GetResult();
            }
            catch (HttpRequestException httpRequestException)
            {
                this.logger.LogError(httpRequestException.Message, httpRequestException);
                throw;
            }
            catch (Exception e)
            {
                this.logger.LogError(e.Message, e);
                // We want to retry once
                try
                {
                    return this.restClient.GetAsync<FullApplicantInformation>(ServiceLocatorDomain.Mpdis, serviceRequestPath).GetAwaiter().GetResult();
                }
                catch (Exception exception)
                {
                    this.logger.LogError(exception.Message, exception);
                    throw;
                }
            }
        }

        public ApplicantSearchResult Search(ApplicantSearchCriteria searchCriteria)
        {
            var serviceRequestPath = "applicants/search";
            try
            {
                var searchResult = this.restClient.PostAsync<ApplicantSearchResult>(ServiceLocatorDomain.Mpdis, serviceRequestPath, searchCriteria).GetAwaiter().GetResult();
                return this.FilterSearchResult(searchResult);
            }
            catch (HttpRequestException httpRequestException)
            {
                this.logger.LogError(httpRequestException.Message, httpRequestException);
                throw;
            }
            catch (Exception e)
            {
                this.logger.LogError(e.Message, e);
                // We want to retry once
                try
                {
                    var searchResult = this.restClient.PostAsync<ApplicantSearchResult>(ServiceLocatorDomain.Mpdis, serviceRequestPath, searchCriteria).GetAwaiter().GetResult();
                    return this.FilterSearchResult(searchResult);
                }
                catch (Exception exception)
                {
                    this.logger.LogError(exception.Message, exception);
                    throw;
                }
            }
        }
       

        public TrimmedApplicantInformation  GetPersonalInfoFromApplicantInfo(FullApplicantInformation applicantInfo)
        {
            TrimmedApplicantInformation personalInfo = new TrimmedApplicantInformation();
            if (applicantInfo != null)
            {
                personalInfo.FirstName = applicantInfo.FirstName;
                personalInfo.LastName = applicantInfo.LastName;
                personalInfo.Cdn = applicantInfo.Cdn;
                personalInfo.DateOfBirth = applicantInfo.DateOfBirth;
                personalInfo.HomeAddress = applicantInfo.HomeAddress;
                personalInfo.HomeAddressCity = applicantInfo.HomeAddressCity;
                personalInfo.HomeAddressProvince = applicantInfo.HomeAddressProvince;
                personalInfo.HomeAddressPostalCode = applicantInfo.HomeAddressPostalCode;
                personalInfo.HomeAddressCountry = applicantInfo.HomeAddressCountry;
                personalInfo.PhoneNumber = applicantInfo.PhoneNumber;
                personalInfo.SecondaryPhoneNumber = applicantInfo.SecondaryPhoneNumber;
                personalInfo.Email = applicantInfo.Email;
                personalInfo.Gender = applicantInfo.Gender;
                personalInfo.SelectedLanguage = applicantInfo.SelectedLanguage;
                personalInfo.FullName = applicantInfo.FirstName + " " + applicantInfo.LastName;

                DateTimeOffset offset = DateTimeOffset.FromUnixTimeMilliseconds(applicantInfo.DateOfBirth);
                var dob = offset.DateTime;
                personalInfo.DateOfBirthString = dob.ToString("MMMM dd, yyyy");
                personalInfo.FullGender = (applicantInfo.Gender == "M" ) ? "Male" : "Female";

                if (applicantInfo.SameMailAddress)
                {
                    personalInfo.MailingAddress = applicantInfo.HomeAddress;
                    personalInfo.MailingAddressCity = applicantInfo.HomeAddressCity;
                    personalInfo.MailingAddressProvince = applicantInfo.HomeAddressProvince;
                    personalInfo.MailingAddressPostalCode = applicantInfo.HomeAddressPostalCode;
                    personalInfo.MailingAddressCountry = applicantInfo.HomeAddressCountry;
                }
                else
                {
                    personalInfo.MailingAddress = applicantInfo.MailingAddress;
                    personalInfo.MailingAddressCity = applicantInfo.MailingAddressCity;
                    personalInfo.MailingAddressProvince = applicantInfo.MailingAddressProvince;
                    personalInfo.MailingAddressPostalCode = applicantInfo.MailingAddressPostalCode;
                    personalInfo.MailingAddressCountry = applicantInfo.MailingAddressCountry;
                }
            }

            return personalInfo;
        }
   
        // we want to filter out some sensitive info using the following method
        public ApplicantSearchResult FilterSearchResult(ApplicantSearchResult searchResult)
        {
            if(searchResult != null)
            {
                foreach(var item in searchResult.Items)
                {
                    item.Image = null;
                    item.DeceasedStatus = null;
                }
            }

            return searchResult;
        }
    }
}

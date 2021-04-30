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
        public ApplicantInformation GetApplicantByCdn(string cdn)
        {
            var serviceRequestPath = string.Format("applicants/cdn/{0}", cdn);
            try
            {
                return this.restClient.GetAsync<ApplicantInformation>(ServiceLocatorDomain.Mpdis, serviceRequestPath).GetAwaiter().GetResult();
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
                    return this.restClient.GetAsync<ApplicantInformation>(ServiceLocatorDomain.Mpdis, serviceRequestPath).GetAwaiter().GetResult();
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

        public ApplicantPersonalInfo  GetPersonalInfoFromApplicantInfo(ApplicantInformation applicantInfo)
        {
            ApplicantPersonalInfo personalInfo = new ApplicantPersonalInfo();
            if (applicantInfo != null)
            {
                personalInfo.FirstName = applicantInfo.FirstName;
                personalInfo.LastName = applicantInfo.LastName;
                personalInfo.Cdn = applicantInfo.Cdn;
                personalInfo.DateOfBirth = applicantInfo.DateOfBith;
                personalInfo.HomeAddress = applicantInfo.Address;
                personalInfo.HomeAddressCity = applicantInfo.City;
                personalInfo.HomeAddressProvince = applicantInfo.Province;
                personalInfo.HomeAddressPostalCode = applicantInfo.PostalCode;
                personalInfo.HomeAddressCountry = applicantInfo.HomeCountry;
                personalInfo.PhoneNumber = applicantInfo.PhoneNumber;
                personalInfo.SecondaryPhoneNumber = applicantInfo.SecondaryPhoneNumber;
                personalInfo.Email = applicantInfo.Email;
                personalInfo.Gender = applicantInfo.Gender;
                personalInfo.Language = applicantInfo.SelectedLanguage;

                if (applicantInfo.SameMailAddress)
                {
                    personalInfo.MailingAddress = applicantInfo.Address;
                    personalInfo.MailingAddressCity = applicantInfo.City;
                    personalInfo.MailingAddressProvince = applicantInfo.Province;
                    personalInfo.MailingAddressPostalCode = applicantInfo.PostalCode;
                    personalInfo.MailingAddressCountry = applicantInfo.HomeCountry;
                }
                else
                {
                    personalInfo.MailingAddress = applicantInfo.MailAddress;
                    personalInfo.MailingAddressCity = applicantInfo.MailCity;
                    personalInfo.MailingAddressProvince = applicantInfo.MailProvince;
                    personalInfo.MailingAddressPostalCode = applicantInfo.MailPostalCode;
                    personalInfo.MailingAddressCountry = applicantInfo.MailHomeCountry;
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

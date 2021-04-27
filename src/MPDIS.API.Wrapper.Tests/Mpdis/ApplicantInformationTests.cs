﻿namespace MPDIS.API.Wrapper.Tests.Mpdis
{
    using MPDIS.API.Wrapper.Services.MPDIS.Entities;
    using Newtonsoft.Json;
    using System.IO;
    using Xunit;
    public class ApplicantDTOTests
    {
        [Fact]
        public void ApplicantDTO_SuccessfullyParse_JSON()
        {
            // Arrange
            var jsonString = File.ReadAllText("Mpdis/applicant.test.json");
            var expectedApplicantInformationDto = this.ExpectedApplicantInformation();

            // Act
            var applicantInformation = JsonConvert.DeserializeObject<ApplicantInformation>(jsonString);

            // Assert
            var properties = applicantInformation.GetType().GetProperties();
            foreach (var property in properties)
            {
                var actual = property.GetValue(applicantInformation);
                var expected = property.GetValue(expectedApplicantInformationDto);
                Assert.Equal(expected, actual);
            }
        }

        private ApplicantInformation ExpectedApplicantInformation()
        {
            return new ApplicantInformation()
            {
                FromAces = false,
                Id = 627,
                FirstName = "JOHN",
                LastName = "WICK",
                Cdn = "00000176",
                DeceasedStatus = null,
                DateOfBirth = -399686400000,
                AddressId = 1926,
                Address = "156 HENLEY ROAD",
                City = "CHELSEA",
                Province = "300_006",
                PostalCode = "B2V7N3",
                HomeCountry = "008_CAN",
                SameMailAddress = true,
                MailingAddressId = 1925,
                MailAddress = null,
                MailCity = null,
                MailProvince = null,
                MailPostalCode = null,
                MailHomeCountry = null,
                ContactId = 1927,
                PhoneNumber = "902-685-2419",
                SecondaryPhoneNumber = null,
                Email = "JOHN_WICK@HOTMAIL.COM",
                Gender = "M",
                SelectedLanguage = "en",
                CdnIssuedDate = 602985600000,
                DischargedBookIssueDate = null,
                SIdCardIssueDate = null,
                Remark = "COMPUTER NUMBER = 00006532",
                IdentityDocumentNumber = null,
                IdentityDocumentType = "335_008",
                IdentityDocumentId = 1116,
                IdentityRemarks = null,
                CitizenshipDocumentNumber = null,
                CitizenshipDocumentType = "030_200",
                CitizenshipDocumentId = 1117,
                CitizenshipCountry = "008_CAN",
                CitizenshipNotes = null,
                PhysicianAssesment = "FitWithLimit",
                PhysicianExamDate = 1456272000000,
                Assesment = "FitWithLimit",
                PhysicianExpirationDate = 1471996800000,
                MinisiterCertificationIssueDate = 1484006400000,
                MinisiterCertificationExpirationDate = 1519430400000,
                MedicalNotes = null,
                Photo = "/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAgGBgcGBQgHBwcJCQgKDBQNDAsLDBkSEw8UHRofHh0aHBwgJC4nICIsIxwcKDcpLDAxNDQ0Hyc5PTgyPC4zNDL/2wBDAQkJCQwLDBgNDRgyIRwhMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjL/wAARCAIAAZADASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD02ijtR1rA0ClpKWgBKWijtSASloooAKKKKYBiiiigAooopAFFFFMAooooAKKKWgBKWjmigBKKWigBKKWigBKKKKACilpKACiiigAooooAKKKKACiiigAooooASiiigAxRQaKACiiigAooooAKSlpKAFopaSkAtFFFMAooopAFFFFABRRRQAUUUtACUUtFACUtFJQAtFFHSmAUUUUAFFFFABSUtFABRRRQAlL1oooAKKKBQAUlLSUAFLSUUAFFFFABRRRQAUUUUAFFJS0AFJS0UAJS0UUAJRS0lABRijtRQAtFFFIAooooAKKO1LTASiilpAJRRS0AFFFFMA7UUUUgCiiigAooxRQAUUtFMBKXHeiigYlFFLSEFJS0d6YxKKKWgQlFLSUAFFLSUAFFFFAwo70UUCEooooAKKXtSUAFFFFABRR3ooASloooAKKKSgAxRRRQAtFFFIYUUUUCCiiigBaKO1FABRRRQAUUUtMApKKWkAlFLRTAKSl60lIBaKKKBhRRRQAUcUUUAFJ3pcUhoAWkoNANAC0UUUCCiiimAUlL2o70AJRRRQAUUUUAFFFJQAUUUUABooooAKKKKACiiigAooooAO9LSUUhhRS96KACijtRQAUUUUCCiiigYUtFFAgooooAKKKO9ABRR3ooGFFFFABS0lBoAQnHagGkOQKM8Z5oAdTSRTd4J46CmM4I4ouA/dluOTR82e2PaoY2xIxqcZJyKQWH9qKKKYgooooAKKKKBhRRRTASiijmgAooooEFFFFAxKKWigQlFFFIAooooAKSlooAKKKWgAooooGFFFL+FACUUtJ0oAKWkpaBBRRRQMKKKO1ABRijmjmgAoopaAEopD9aUHNABSdaUkflTC/p096AEY8c1EZOOAfqaikn+fAOT7VG1xlADxz2qWx2JQ/zMDSM2AuMc1HuDHHXFMDjauTzSuOxaU45I6ipkbjiocqY1p+7KfLTQicUtQxNkde9TZFUhC0U3PNKaYBRxRS0AJQaKKACiiigBKKWkoAKO1FFAhKKKWkMSijmimAUUUUhBRRRQAUtFGKBhRxRR0oAO1ApaKAEpaKKACiiigAooooAKWkpaAE5o59aKa7hFyTQAufWjOKrNc+nSoWnHUsTSuFi2ZBnntR5oY4A5qgLjc2DjHahrhkbt6ZqeYdi75u489BUEswJ749BVY3GF9apm9G/b3Hak5FKJYeQCXGcVFcSIq/e/Kqs9yqthCC3c1Qnvc8cDB/OobKUTVinJ6MRj1qRJAQCT0rCjvThuhzT0vtqEdyKXMVyM6aKZDGOuRzipUlzGT09q5Vr+VUATGT60fb7zorL9DVqaJ5GdSrhBuLcE5xUgvVHVGNcl/al2g+aNfrTE12Tdg8H3oU0HIzsPtqbuQ2D0qZZ1bjNcmuqMxHTNWI9QccAjnqKamTyHUBuOKdWRaXzHCkitJJNwq00ybEvFFNB9adTEFFFFMApKWkoEFFLSUDCiiikISilooGJRilooASilpKBC96KPrRQMKKWigBKWjFFACUtFHegAoxRRQAUCiloASiiop5NiZFADZ7gJ8qfM3t2qo5OMyNluwzTfMxnAyx5NAiJG5iKzbuUkM3Me5pjBjk5NPkkSJclwKqSXyDOBj61JQ2R2jOc9KgmuyzgZNQz3W9SqnOarANu3Oe2ABUtlxjctyXO1SA2T7VSeSXHGcnvUlHXpUORtGmVwsrHJzTXhPdSfwq8oxTj0xU3L5UjPEOBwKRYCeW6Vc479aCnHFIqxXMY420HmQZPAqwRwOnFGwY5oTE0QSDgjqarvaKycgA+tXygI6dKZs5461SZLiYjJNakkMSO4q7aXyTKBuAPfJq1PbiYYxg1g3tnLaSmSPp6VSM3E6e3nKODnv61v21zwCGBB964bTtQWZ9jZzXQWc+0bCauLsZSidQr5GakDjpmsqK6GwAHmrZlCkZ656VqmZNFyioVkDdakVvSquIfRQDmigBKKWimAlFFFABSUtFABSUtJSAKKKKAF5ooooAKKWkoAKXrRR2oAMUUUUAFFFFABRS0hoACcCqd0ScL69atEce9U7x1iQFvxqZbDRUdyoIX86qy3YVeZM1m3l9I87CIlIxWe8sjttUE/Wsrl2L818CcKM+5qk7NK3U5NNEZ6yNn2qUEKOBUtmkY3HRxiPknJqU1Fup24+tZNnRGNh+PelHFRsSehpQSetK5qkS5I704MMc0wY9c0vHWkPlHH2puc9OlCtuOKD6DpQNIRjxTd1SKv40hXnGKBNIQOBxQR3ppHJ9qTfTTJcRxyKjljSVSG70pYZpu7ng1VyXEwLiyksbjzEOVPUitayuAY1bnP1qWYJKm1xkGsvDWkuF6E8VaZjKJ0azH5SDjmr328L1BY1ztvcNIVHYVajcvKFJ6mqTMXE6W1vUlG3GGq+M8dBWNYKqHgZatdSD7Y9a0i7mbRMrHv2p+ahHt6VIvStESOz7UtJS0wEpaSikAUUUUCCg0UUDCikooAdSUUUALRSUUALRSUtABRR7UUAFFFFABSE88UuaaTzxQA1vc1h6xIZHWNDwOta1w+xCSelcxcyGa4Zs4UVnJlRRUZERi0hz7VUe8bftUDHpU1yeqoeaqLCYzljkms2zaKuSrIzdafk1GBxxTt2O1Yt3OqMUkSL0qVQPxqJDnmpEJLc1BaQ7AAzSZ9KUkdMU0ZGRjrQaRJO3pT05qAPjjFTq2AKBtEuD+FKV4zQr5p27qDQQMAzSMpp3TpTc5NMZGxPak571IV4ODTAQaQnsRPyKhzjrViRSDUD5qhWGMQarTLk89qlJxSOP1p3JaGQAISxJ29qtRgcd8HPFVl+U7fWrcahWyDweorRM5pqzL1vcNGd3OQK2YLsXEfJ+asNAAG7jHSp4D5ZyM4P6VaZi0dJGcqOanTpxVGymDjaetXlraJmx31opKWqEFFFFAgooooGFFFFABSUUvFIAooooAKKKKAFooooAKKKKACiiigBDSc5peaaelIDK1CXMmzoMc1hXbbW2rgVrXzD7Qc1iSNvlJPTNZSNIkDYVdxGSap/NI24nNOvrhQ+AelQiUcAGspM6aaLOeKAMtUYzipY+OTWR0JWJOg4FPQmod2SalVht60FIfySaUdKjV+eelSq+RRYtaBtIweuafxjpzSrg04bT0osFxFOAOOtI7Njin7Rkc0ONvAp2DqMQnb81OUZB5pu09e9HOBSGOLY4qME5oYEn2pMdqEhWFkYFfeq+eSKkbNQtwcjNBNhj4FNB3DFI7UwHmmJokYYXPpT45SRyahLEVHC+JdvrVxZhUjdGokw4PtzViByeM/Ss/G0bqktnwxU+uRWpzM6C1kIZeenetqNtyg5rm7Z+eK2rOXc2ParizKSLwpabS1qSLSUUUALRSUtACUtFJQIKKKKAFopKO1IYtGaKKACjvRS0AFFFFABRRRQAY96a3Q06mtSA5jUZMSyN2ArFM4Az7Vr6quGmA7kfzrnbs7FG04rB7msStdfPc8dKmiQY6dKo72kuCM5Aq+nArKT1OyC0JR/KguAuc1GzgdKyL7UGEgQcL3qUjUtzakY5dipu981GNYGcGMjHfNUWbzQCv3geopBZuTnOVNWkibtGlFqjOTtXI+tTnUZQcKAMdc1n28XlEg8+9Mnk/enB7cU+UpSZrwakxcK4xn3qa3vD5jg9jXORu4ljySVzmte3Ybyc5JGKTWhaZvxSB1Ug8mpgwIOeorPiJRBilErZzngVBVi6GB7UhZQCScVU844yDWbd3xjO3Oc9KaRPU12nTJIIpn2mMHqOa5S51WQDCsfeq0N/dSMQpIPXJp8rJbR2DSZPy00tkEGuVOp3cDjLEj6VKutSs3ajlFzo32BBpnQ1Qt9SWSQK3Gf0q/nPIpBe4rjjNVOVk3D1qyTzUD/K/1poiRppiWHPtUedrLio7KbEpjPQip8fPitUcj3NOzbJHvitixP8ApBGegrIsPvKMd627NQJmPtVx3M5Gl1FFAorYyFpKKKBhS0lFABS0lFABRS0lIAopKKAFopKWgAopKWgAzS5pKKAFozzSUtAB3pCaQmq8swBOM5pXAxdZjyXJ4rjtRlAIA5POcdq6TWr4uGU9q5SbDbiKxe5tBFe0OCSx61po/SsdGKsQe1XYpD171i9zvitCS5l2LnOOaxZszykjke1X718x9OTVW1ABz0GaNhPc0LO1QopZcNj8617e0jPYCsyKdYwMmrK6rAvAJZh2FIq2hpPpsTr90D6Csq40bDErU760VUYifn1IrPn8RFG+aBx7jBp6iVyrNbtB95cAd8VJaSgNjd+tLLqUN1Cfes6NiLj5T8ppvYuL6HWQvuj4NEgKj0pmnpuRSfSrtzAdmfasrl3sZTzFFIDYHesa8myxw305rQvsohwOtZEUElzLgVpEmRTKuzE4Jyau23mIuPLGe3y1vWmkwhVLEZFacFpaq3IFVzGRyrWt1MOIyQOvy1Re1mSQ5gb6gV37xRgfKBVC4gXDDApcxPKjk4wV5w2fWrtpey7/AC3JxS3SCNDtGMVQil2ynvn9KL3HsdEj7hkmopm5BqGKXgDPaiZsCgbVydJdjI/vg1qIwYhh3rDZi0PFaGnyFo8elao5JrU6fTEy4OK3IF29qxtKOeK3E4rSCMJPUlFOpmaWtCB1FJmigBaKSloAKKKKBhRRRQAlFFFIAopO9LQAUUUUAFLSYooELRmkpCeKAIpXIHy/jmsS8uWTdzzWrcuwT5fxrlNRuMTsM5xWc2aRVzOuZGlkOGGO9Z8u3DdcCrTOpOcBf9o1UuJ48bQAayNooz0G+ZuuM1ejQKMUy2UOS2B1rQ8tccLWb3OyOxn3MeU981lGfyi2SBg10VxHmLOBmsD7D592284A7VKkDRQmvX2M8j7Y+wHU1SOo3bRs1sAiDvjk11X9gQSKCw3H3pyaGiDasa7T1AFaRlFbg4tqyZxsN4ZLe5+03FyLr5fs4QDaeTu3fpjFaVsbs2e52DexFdNDoNqjbnhj9siny2yKdsUIVR2AqpVI22Ip0pp6yMTToUuSQyulWvJRLnamcD1rQ+yGNCfu+lJHCiAlsZ65rncmzpUTWscJEoq1cyDZn0FYsExC/exzViW6JjI9qBuJSvH3is6Eyxs5iXODU67pmbrwelSRKY9y7SVbrxTTsTKJRfUbskr5ioo70xNYSFsy3zZ/Cpl0tGuS5Yt82drHisnUdHu7eXz4bdJMNkBow6/iDWsOV7mVRyivdVzeh18lSYpklHfPBqddXW474fuK4S1026ViWEiY5PBFT/aJon2OsgA6SYNVKmuhEZuW6OrnmEoOazkH7/K/jVWG+ZwEcfMeh9atWqs75A6mo2G9TUj+4KimkJGSasIm0DIqtMAo5/GgfQtQfvIcDtU9sxicA9CaqWkqBsFuoxVlUZjjOTmtEc01qddpUnzLj0rfQ965TSiVZVLc10fmhIwdwrWD0OaS1LqnNOqCF9y5qYGtCB+aKaKdQAtFJS0ALRSUtAwooopAJRSdqKAFopKWgQUtJRQMWkopKACkbpS01qBFK+OIuO9cdfOPMY+prs7pdyYxXGasvlSsTwPSspmsDDvJfmCAnk1DdlLeBS3LP0pX+eYEnJznFUtSlLvGmfWs+hulqW7GYbQfWtZHGKwrf5FArSifJHPWsWdiRbJ8ztVP7Mwl3Aj8aujgcU9VDD5qgpRIFaRO2fpUitM3Odoq2kKFeO9BhjBxzRqUiKL52PVyKn8sJyw5p8EaIfl70s7Y3L6CqDqZkrb29RVS4kwNvTNLNOsBZs/hWe1x5smaEii/B0BIzUs8oKnjtUVmQzYz+NPuNqhl5zRYuxUtpAkp64NXRw+QODWWGw5NXrWUSRHJ+YGmydC81rvAdeKnVMQ4kAK9MinW/wA8eCe3NEnmQD5F3L3U0lchplV9Pjk5VhiqkukREYIBFWzcRs3yZjbuDVaeedSQoDD1o5mTZmHe6XHEN0fBHYGr+m25Cgk0oikkkBkPHpV1AsZAX9aOYXLYfImBng1m3K4VjWjLJhSay5WJRsnkirQNFOCQ7mUdc55rVguSrLvzntism3QvKcmrsqjy845XvWqOOS1OrsrpcKxHOODWlHOzuB1rl9MlYoororJwHBI5prsYyR0dqD5Yz3qzmqlvKWSrIPFdK2MHuSClpgpwPNADqM0lKKQC84ooooAWikpaQxtFL7UlABRRS0AFFJRQAtJRRQAUw06mE0CI5BuXpXD+Jy0c/A+U120kirwxxXN+ILQXELMp68g1E9i4bnCs/lv16iqZ+ecl/wCHpUt0CXGKiwVb8K5rnpuFncsp2GauxvhhVC25OSamZ+eKh7GkdWa8UgI6ipFPNZsMuQMVdifjnrUlo0Y/u4FSd6qRSgcZ5qbzAPc07DsWYdoLNnFV7p0GSSM1HLPhQBWNqN4QMFqu2gakc6CclnX5ag8pUf5OMCnxXaGPDVXlnRZeD16Uhq25o2cZ35U8k+tWLzYwYAEMBVK3uAMMpANST3itk569aC21cpSgOBt4NRnzoMMu4g9aj+0L52Ac+1aQdHiAJp3M3qaulz7o1znp3rQYbjzWJp9yuRGAflP51s5yRjg0mhkLwIWO9AfQkVH5Cc4UVYllyucEkVX8wevWpaC1yOSFFGRioDjB4qWdwVzVUyDB9aSRFiCWTnB7VmyP97HNW53Ge9U26k1oiGQ20myQFuAeK0FZfM55B7ZqltXgkZBNI0rNKCnGOK1T0OflbZ2OnWu6NGjX5SOuK2Y7Zo8EDP4VV8Oln06PIroNuUxgVrGCaucdSTUrCWe7Zkmry9KqwLgEe9WBwK1SMmyQU6mCnCgB1OFMp1IB1LTaWgB1FJS0hjaO9FFAgooooGFGaKKAA0lFIfagQE0wninVFKSFNAFK8cjuAMd652+nco6hiAO2a3L6ULEWPJ9K5m8kGx2OMkVlNmsFqco5DzAVTd/3jADvgVbk+SUGs4sfMJzjJrmR6k+iL1sSFqUtzyKgiOFHPamrKTIBmh7BF2Zfjk9qsrLgVnbyq8VKjZSpNUaKzYIqVpyozWcGOKk847Oc1SNCWe6ZcGs6WA3IaSRsDsKsfLIQWPAp0gDRYGMHjiqbJOUnuJY3KEnAPUVD9obIbecj1ralsQ7fdBA4+tMbTICcBfmqlJbHNJalOPUXCAY5pkmoSHtyfenywrE5UjFRtCTgKM/SqSRN3cjQzl9+8CrkWpSINjKCfUGqhtW9SPxqWG0bIPJ9aG4jipX3NiwnmSRZCMBjmuniuw6dKwYUBhTjjFWIpSnyms/Q6Eu5qGTJqGSUA5FVjMSRg012PbqallkkkvBzVXzRnGaWSTap5qmJR5nXNIzbJpSCaru2AeO1TMQelQScg/SrIe5ADSpyQB1zUEbZnC5q7bpm7CDkmmRGy1PQvDsZGmx89eelbqggcVm6QgisYl/2RWmtdsVaJ5M5c0mx6jbUgNRg+tPBpkjxS00U4dKQDhThTRSigY6nU2nUgFopKWkMSikpaADiiiigAoopM0AFIaWkNAhtV7gjbgnFWKq3JyBQwRhagwyq+lYd+mYnIHatnUgVkBzxWRcTDY2R29awlubw0OYugGjB9Ky0++fc1qSxkggN1rKClJCD2NYpWPQk+azLwU+TuHaqcb/NkDrV2P57cAYHFVeFA6E0dAe5MCxbB6VYjz1qoJHPUflU8L89c5qWawZoKMgZpsjqv+FSxcgL7VUuuWKqeR1oTLk7EM04zx2pILrONxwBUBkH3SADRuQLkmqMuZydi6Z03rt5JPSkaUedkrjiqEd5ArhiSSKnW/gZsFfvdTmp2HyXJvsqSN5pyarYWOYpjr0zWhHJblDicfQ1A7WuQzOCaakxukraFPKrIQygKeM05mQkInWn3EMci5ikGc5Aqq0Dj5s9KrcyalE0bRyilN3X1qwD/ePIrPgcKp3MD7Yq0HQ4ODnHPNJlxndkxkyQRzSGQkUi4ZcjnFI+QM4pM2IZpcggVDCR82e9JM3NNVtik00ZSepK0hHGOaY+4JyOtRh9zZ6CnSuCvBPSmQVY/wDj438jFaelsPt6SH7uazUG44JrRt3WFg3YU+qI+yz0q0uo3VAGHQcVpIwxXD2F6CVzzXV2Epf34711xlc8uUbGmpzThTFp9WSPFOFMFPFADh1pe9NHSnUmMcKWminUgFpaSikMKKTtS5oAKO1FFACUUUUCCmmlNNNACVBcLuTp0qc01ulAHO6jEjLznNcxeD5iinpXX63xApHBrzzUJLhLllJIU96xktTaOxVuSbckk8Vmbg7Fh0NXpE3QMScn3rORcMV75qXHS50U6jvyl+3wV68AVFsGMjHJxUSymIjvT4W2sQTye1ZHRcl+VRnripY1AbI6YqOP5pCBip84Iz1PUVLNY7mhbMCvXBqnOuwN3yalgbBzmlupV2EcA0RKm9Dnb2SVnKxdazpPtzHac/hXQrCWy7Cljj+bpVOpYzjTb1OcW2u2PLYq1FYXb9JB+VbxiLdVGKUIsYzjApc7ZsoR6sxnsL1BjeDUbWd8i5yK3/tELDB4IqMsjAnNPmfYqy7nNtNewH5lP4VJFrUijbLyPcVtGFJDlun0qJrGFifkz+FHMuqM3F9GUft8buuCOa0o5y6AA5+lVW05MgBKsQRC3kAPAqrxaMXFp3NG0b5SD2p7txyKmfy/JDIOtVZG5xis2brYqyuoByKgZwowtTv3yeKr/Lgk1SMpMaNwCkDg0rk56U3eNmOuO4ppbONp4xVMhMkgTdLnuBWgkQcgEYFZ8UqoeTzWtayRiLc7cntVRWpjUklGxpWcG14/QYrrdODJjniuZsS0ssfGF6CurtIMDJ6VtBanHN6GmuMVKKiTgVIDWxkPFOFMpwpAPp1MFOFAx1LSUtIBe1LSUtIApKO1HegYUUUUAFJS0hoAO1NNLSfSgQhprdKUmmnrTAy9XhMsKgdjXN3ekeZFuwDj9K7OaMSLg1Se2UAg9KiULu5alY8kvw9sGU54NZSzlpD+dd34h0YFW+X6GuGuLCS1nBIwtRfSzNY73JRyf1zQj5Jz3NRAnoDwKduKnkfLWLR1plxHwvy1YY/u898VRik5HPGalmlKsNpwNtTY0TLsTsI1PtSyKJvmJIx196rJIxiUDqauwxME3McmhFN3IC4VQvpSxkHinSqAD8vJ6mo0woAHX6VEjZS6E4Ge+aSUbV57CnID0plzk4Uc8c0kKRWVUlYP0GcEU4KN2B2pqRmPOOPYVOgXIyfmHU1pcyUXckWMNTWTDYFTRDc5wRwKjlBVvrUGyIh98c9KWSAS8Hj0NR7lMnOV9amjdCWEhO0dDTSIlNDIGYK0ZJODTbiXCYB5qVfK3Hafvd6oXIYTMoIOKdtSOa6BpeeR1qvK5PAHFStgKCe3eo9wCgjntVIhvQjwVHfBoxtH4cUr5PHQelMkyMDP0qkQ72GRyDeQQSwPFbNorzFcrzngVUsbEyuOMsRXb6HowVBJKuWB4zW6TeiOKUrblrSrAq6PITnA4xXSRqAKrxx4ParCd8VsopGDd2TKakHSolPFPFMRIDThTBT6Qxwpwpop2aQx1OpuaWkA6lpBS0DEooo70gCiiigQU2l702gApppTSGgBKaTSmmGmICahkKqpLkADuTTpHWNCzkADkmvJPHXjs3Msum6azLGpw8ufvfSmgNfxh460q3tZLeydLm4PAZCCF/GvKpNbvLy6Vppztz93PArPbLE7aYE55PNVyIpNo6vcuxXDDnsDU5l3IFIyfWsu1Ia3Tnpwaug8e9cclZnoQ1VyYSEH6dqn3h8HrVBmJfk8+tTI4B56VLRSZo28gBIPJPAFaSylUXsvrmufjaRZlIbqa1oJBuAclgD0o5QdTU0f3XLA7lAyRiq3liRmbdtHpVjI4AG1epz3qGRlcsF7VDRrGQqFQvzN1PShiB0wcGquXbay5+U96uRRho+eMdc96mxomRspZC3HPpUWWDAbRx1qy5XYVUfSoidoBbvwaVmPmQiEq2V4+lObLkkHOKInVM7gSDzxSgq7HblV96aRLlYi2tnPX2qbyQ8R3YUimSROjqeo68elLMwSI725HSrSMmzPllaKN9nRfSqaXBd845Pc1ZuLuIxFQuWIxwKow5AzkGqaIUr7EhkLqVAOTwAKcImC4PHsaauEfOeR0pWdsnnNJIb1EZtoxnnHBqjd3DRJu3YI5FWJJR3FY2p3AxsIPNaU43ZFV2idFpfitNPdZJYhIuMEd67jSfHGiXaYe5S2b+7IQK8V+8mAajKuD1BrsSsec9T6WttQsrpVa3u4Jc9NkgNXVxXzHb6hc2kitFNJGynIKmu10T4m6hZEJeAXMYH/AAKgmx7WtPFchpHxC0XUyELvbyccSgAH8c11cM8c0YeN1ZTyCpzSGTinCmCnikA8dKdUdPzzSGOFOpopaQDhS0gpaBhRSUUgFzSUUUCEpKWkoASmmnU00AIajZgASTgCnMwVSzEBQMkmvMfH/jRPJ/s3S7r5y37yWJ+g54yKoLXK/j7xs+86ZpsihcETSDr9BXlzEuxzz6mpHZpGJYksepNIRtXgfWqLUbELAKOKrkkVc25WoXTnpTTFKJe02XMe0+taat82KxbEFSxrUVhkVzVV7x2UG+TUsMv8VAZmO0CnLhk65NR4b3BrJGkiaR8RgqecYzV2xuldFBOGFZpU+hpEPljK5Bz1q4szlF9DqBchvlI3YHJPaohkPhehHJrJtLp8FWOTnGc1eN0QwQsNo96UoijNrRk8cvzKCOD1qUXKKSnUVQS5HRRlwaaXz8w6jrUcpt7TQ1Y5BIy4/OkkVtp3Z5NUobhTEMnbt70k14bn/VNhc8gGnyi5yzx/ew3ertuqpCTkFqymuFVFBAJx19atQTeWing5XJBpcoOoXZJGCjIHA4+lULmbdDl8Z7Yps99thyWyQCCM9KwWu5JH5JKitFGxjKTlsOkbMm9MccEUEHZnkH0FRKdpZscmpCxK5NTI1hGw/BVMnvTd5XqajeQkimMflznpU2NEgmfr3rF1IkuCea0ZDk4BBrOvxhQa2paMwrq8WUN22kcnqDS4DUgO04PSus4CSJg4wwqYwrj5SQaqHKtkdKmEwI5PNKw011JP3kXI5rf0Xxlqui4W3uMp/wA85QSK5/f0Ktmnbtx5UUrDPavDvxKtNTmS2vo1t5mOAw+6a72N1kQMjAqehFfLSh4myCce3aut8NeOdQ0STY0zz2xxmORicfSpYcp74KcOvNYuheI9P120SS2nj8wj5oiw3L+FbQpCHU4UwU6kA6lzTadQAUUUUhhRRmigQlJS0lADaa3A5p1cr461m70fRPMtY8mQ7GfH3R60Acz428eKFudK08Pvy0ckvYdjivK3JLepqxI5mkZyxZmJJJPU1G8f507m6jZEYXAznrSMKU5HFNpgNxxTGH5VITSEZFNEvUfZLkutXu2aoWbbbkj1FaaryRWFT4jpo/CLC5VetS7/AJcioWXYuRRG+PxrM1sWUyRk/lSMAy9MHNNDD1pVY7h9aRLQ3BC/JndSl2IUE8D1oZmD5BNROCTzz3q0yHG5ZExjkZwuM1Cb8pvypIY0zeSRTztbhgKq5HIP/tAGHGwjPFJbTBUxzuNRMi55xxQpGMnt0o5kCgzQ8+PgGnzXZbakYIA7ms52GBigSNuBPHHalcrkJ2kMhO485pPLVF7VGMIxyc5pWYdAahs0UUhxI44p3G0BsCmA4PzYpsj5GD2pFobIwZs4xULtninbsio24plDVGXz6dKp36sycDpya0o4/kHvUHliSVhjI6VUZWdyakbxsYIODmpMB1qe+sjbncudpNVUautSUldHmyi4vlkBQqcUwxkVbA3rz1qJwV600yXEiVinWrEUgxtNR7Qw96YcqcYoC9i+r+pprAMcqcGo4fmSnFSOaVi+he0/U7jTrpJ4JGjlQ9R3r2vwt49stb8u1nzDeHjBxtY+1eFhA6hicEin29xNaTxyoxV0YMrDqCKmwNXR9Sg06vNvBfxAfULhbDVDGjbfklHG4+hr0hSGUEHIPepJsOpaSlzQIWiiikMKSiigQhpKXpSUANNUNVW3ksZIriNJI2GCrjINXJZUhQvIwVR1JNcV4i8eaNaB7dC9xKB/yzwQPxzQ720GvM8r1zQr3TL+aWK3c228sGQfLtzWcHWQcde4rR1bxRcX/mDJSJs4Qelc6JzFJuwcZ6URUmveL51F2WxddcEd6Y2O1Kk6Tjjg+npQy81Wxd09iPGeaBTgKKYiHf5dwrVuxDegbrjrisR49zqR2Nb2nqTHweD1FY1mrG+HTuwkjyvSqxXa2e1a3lfLgiq01viudSOzlKeTjvT9xxwKa6MvWnRdR0q7mbiEhJQYqMOQOlTSYYZFREHhaaM7DeewNHzdeaeBzx+NP2Z46UXERYJGSKR84xipth3YBH1pWXC0DKwBHcmnB+c4qTZx9aZjHbvTGh6kZz69qM9aiGaUA5pDsSg8fMeaa5yRtFKqE1L5Rx71N7Gijcg296EjMkg44FTlMVLDEcc8ZqXIvl1I5PlXio7KIs5OO9WJY8AirWnW+EyRnmp5tByjqQT2SyxFSgP1Fcxf6dJbSblU7fpXoS2+fpVK8sFmUrgVVKs4s569FTRwcL84NSlQwPrV260wRSOOAwPFUUJWUo3Wu5SUtUcLi46MiI2nninAK4560+VMjNQdDxVJkPRkkOVYg1Y6g1XLZXcDyKmjcMMihjXYVCQCKkU54PNRfxmpkGKllIVN0Um5WIweoNeleB/Hhs8WOqXDPEzfJLI2dvtk9q844prBozkdDS3G4n1FDNHPEssTq8bgFWU5BFSV4z4L8fHSVisL/c9qSFVx/B/9avYbe5iuoVmhcOjDIIpGTViekoopAFJQWAGSQAK5/XPGGlaIn76dXkOcIhyaBHQVz3iHxdp3h+AtM4eXB2xqeTXmOs/E/WLmaRbGRbeAn5cIC2Pqa4a9v57p2luJWkkPUsaaiwudR4n+IN9r37mNTb24Odqtkn61yDTs2c857mq6MWJJNOBya0skSPzxzTevJoNIT2pgNyY23Lx61einWYA9G9BVBqarNGcqcetDVxxlys1SB2ptNjnWY5wFPoKlx3rN3R0JqSuhij5q3dOXaorFAwwroLJPlGa5670OrDrU0Ui3rxwe1Mkg3ryMEVYg4NWJY1ZdyjGetcqZ1tWOfmh6jHTvVURYNbE0YyciqcibT2rRMGkyjjA70oTIHPQVOygjFRfdaqTM3HuNVcN61OEyO1MUg8VNt4o5iXAYY1AyTmoXIUHnNSMuTzUbqBzinzC9mRgk9OKApJ9akXH+FOQH25obLUSPy8A81IkHTPepkjLdqmVAGqeYtRQiQgL9KaU/OrKxlhgD8anW3VcZ5NQ2UvIorbFuTwKspBxx0qdlGcAcVIIyF4GKhstKxReHca0bS22IMCiKAO+cVsQW67MYxUNiehXWIYzioJowT0rUdQi1SuiqIXOBgU0Zs5DVVAumH51z9/H5bLIK2LyUz3Uj543HFULxd0PToa9ClpY4qyuijGwkQ1E6YNLFmOQr61LIuRxXQce6K6nBwali+XOKiIwcVKpA5NALclHXJqdMZqsG54qygqWXEcOtOPJo+goHvUljNpTkHiuv8IeOLjw/L5M4aezbjbuwU9xXKex6U0rg8dKBOKZ9R1S1TVbPSLRrm8mWONR371LfXsGn2j3Nw4VEGSSa8B8beLpvEOpP5ZKWqHEabuvvSSuYMk8U+Or/AF2SSNJWitc8Ip6j3rkt7E8nNNOdlNU1olYm5KWxyaqOSxqWTrjNRgDPFUhMcBhaF9aD92gcLQAZ5pDRnikoGIelMY4qQ1DIecUyR6naR1rShmEi44z6VmA8c1JG5Q9/fFJq5UZNM1A2PeumsFLIpHSuVRtyg4x7V2elput4x3xXJX2PSwzLqxso7VNFjHzVKsBIxnNMMRUYx9a4zvVmQXEOSSoyDzWdJb91rZUY+X1qJ7fb83UelUmS0YMkBXqKqy/St94gTgjiqc9iG5Bx+FUmJoxVbtmp1fC9abLbGM5FIqkVWg0P3EmmtzS45BNSrAXPHelcdiJE6cVZEbNyFNTRW4RhzmrYGF7Ck2CRXjjcLzwKmSIDk9alCgr7UqIWO0CpbCw0Kc4HNTBQq4709YCeM8+tOMYA9aRSQxUBxU3ll+3FPSM9auRQ5AGOtQxhbQY28YxV3b3BqWGAKv4U4xYzSM27lORT6VxnifVFW58jfhVHOK7ebpx+NeZeJ7ST+1JDnryK3oRTlqYVpOMLopi9hZwA5B9xUkjB0IH4ViMpQ89RV62nLrtbqK73BLVHBGq27SK9yhUqw/GpYjujFSzIGUiqdu21yh7VS1RDXLIWRaYOmKsypnkGq+OetCE1qPQZxV9MBKoDhxz1q4h4pSKgSZFG7tScUu3JqDQNxPSnjPU0qgCg8/SgZ1/xF8Yyapd/YbSUi1iJDY/iNedFtzc06Zs81EPvVolY4yV+EpsXTJFOk+5SR/d6UwGsaYoOac/WhRVCFIpDTjTT7UhjaKMUv4UAMqI8vmp34WoVFMkX2qbbwKjYc+1SIcrx2oGWLZsccV3OiXMUqIo4I4Irgo22sc49atxXTKQUbGD2rCrT5jqoVeQ9YRRnI6UjxZyRxXJ6J4n+zkJdkshHBxyK6m3vre9QvBKGB7dK4JRlF6npwmpbERiw45xT9uR05qYpvOMdKiCFTzUmu5E1ruJOKhktwOSK0B2zxTiFIouFjn7m0jIwRzWbNbFW6da6qSEYzioWgDAAgcdKFIqxza2wxkg8VajhBHArYFkrH7tSC2ROBT5gsZUUPPIqwsHH3c/WtSGKNDk4JpTnOVHFK4MzBbseCuKcIGUHAAq9k+lNJJIBFK4WIY48dTxUgiGc4qRQBS5ABApNisEUXzA4rQhj4xiq0YPFX4jgYoJkTIhxyaafu80SzwwRl5JAoA6muY1LxjapEy2hMj9jggU1GUnoZSklubd2RHGWPFcFrs6SXYK4YgYJrNvdZu752M0p2+gPFUId28sOlddOi1qznnVWyH3EKOT8ozVDHky56CrM0jySHmoHi+XJrqjtqcc3d3RZLB48isxzsnJGetWIX2hgTxVaYhpSQaqKsROV0i+AGT61AyYaltpNwwTzUko6GlsylqiPbwDjpVhOlQ/w1MnNJlR3Hg1L2qBvlp6NkcmpaLRIWwKYXLfdp4QUuMdqQamQ5yetCcmmnrUiDvWxyCyfcoQfJSycilH3elIZGRk80qjmlIpBTEPIFRkVJTTSGM20oHNITTlFAiOfgAUxBzSzHMgHpTlGMU+gbsbLwM0i/L05p0/3BTUOMU0InjwT1A47inW/NImN45x74otj8+KllwepdX72KvxXklqU8pyCPQ1SgAaTmnPzL1rKSTOqLaZ1+n+J8GNLlOuBuBroRKk4DxsCp7ivMdxLgZrXtNZmsCAjZToVNc06PWJ2U6+tpHfph2UcD3p0sQQjBzWTpusRX2OQpx0JrT3de5rnd1udK11QwjIqNxyCDipsbmxj8aUImfm5qSkyEc98E03HByKlfAPFIemMUFXIsEfWl+6P50/aOtNZTnAFIQ1pM9qjOTUxQgUeXu4oHci2n1qRI2PfipVix14qjea1Z2JMW7dIOwoUW3oRKSW5qxxYXPQetUr3XLWwGzPmSegNcrc+KL25by0YRo3HArHuZn80ktya6IUHuzlnWV7IvatrN1qE5DEpHnhQayTGwbaO9NaR5JMDqanlcwgEnp3rpUeVWRzSlzPUREiDbXH41BPIIMquDn0qtPcl2O3iod5I+bJzWqj3Mp1F0H+ZwTmm73kXaRgU9Iwx5NTCE49qp6Gau9yt5KkdcVWlQI5AOa0Su081n3H+uNEW7inFJCRNskBq+3zID3rN71ftnDKQeopy7ipvoKFwvNPi5AoYYFJF3qTTqOl+7TYzSy/dqJG+bAoWwN6l8fdppbmmxnIpxqDQx6mQcVGuSan7Vqcg1xTsHFMbrUg5FAEbCmipGFMxTEOHIpDQD60NjHSkMjPXFSDjmowMtT24Q0AVs5kJqYdBUKdan7CmxIZN9ymDopp8/wBymAfIKEIsRnDcMRx2pkTBZOadAAWALFfcCowMy4PrQUmaEMiDJLdqkDR9RIPzqmIm6ZpwgA+8ahpG0ZS7F+LYWzuHHvSgbnJHSqPkg/dOKAJofmBJpcpfO1ujRG7eNpP4VvWevS2apFId6j16iuUgv2jb5lzUv2+OR8spGazlTvuaQrpPRnptpqVreKPLlXd/dzzVwgYrzO2uSkqvDJgg8c11lhr6gLFdE7j0YdK5J0nHY7adZS3N7bg0vlsOSCKSOVJgHU/KemKlaZnwpHSsjfUiZcLTQvc9akcdBij5VGTwB1qbBcETPJqveajbafCTJIpb+6DzWXqPiOJC8NvuLjK57ZrkZbmSa4LSsST3NbQpN6sxnVS0RqX+uzXsm1CUjFYrNtlLEE807awfjoDS3MbHD54NdUYpLQ5JybepB5jbvumh0d/mbgH1pXmWNc56VXlvS6bQOK0SfQzlJLckd44R94Fqpu7y5yc0zhmzk5NSJG27irSsYyk5aIjEbg8A1Iqc8rVhFYHpU+wsPuUnIajYrBAfWnKNp6U8ko3Sk37hkipuzWyHMI3HXBrLul2zdavnBHBx9ao3a4cGrjuY1FoV6khbZICelR0VoYXsaaurnqDTI2BZgDVSCURtk9KsIy78jvUWNlK5YblcGqRJSTmrmcGoZ48jIoQ2iaB8gVORWbBJtfBrQVwRUyRUXcy161LTI8GpDVnMNHLVIOlRr1qWkMaaaQTTqM0AMA7Ukh4p/fpUb8tjtTEIo4+tJMcJipB/KoZiC2KEBGv3qsY4qBsgAipgcqDTYIbP9ym87FwKdKPkpE/1fTpR0C+oisVkUZP4U9htlGcjnvUUhwVNSykHawB/E0AmX1VSow1L5ee9Rw4ZBn0qUAeprNs6krokxHHg5yaQuWGMCkIX1zQP9kY96VyuUYYQwzigW0apk9alVXbseKd5WRgkc0czRMoxZWWE79yMQalN1cRkbsMB3pGUqcKfyqSNkQZk59iKNwVlqtDa0nxM9uFjbBQdj1FdvDeRSwh0YHIzXljrHK2Y1Cn2q1ZXV7bSA+e4Uds1hUop6o6qVeS0kei3V/Fbxb3b8K5LUPEE9yzImEj6YHU1nT3U8rbnlZhnuahllhKbiQrD3qYUrblTr/cIHKybj65qWYIyCQVmyXvG1V+hqHzZGGGZsGuhQZyyqq+hdku0C4J59qrNfS7SqgYPrUXk7uc/nUiwDPLVSUUQ5SkV9zsee9SrHkcirIijHcVL5cePv4o5uwlBbsrrbxhRyaeIwBkVJ5Qzww/OmtE3bkVLbZa5UIzMo603z5F6UpicDoaNrL1H6UaDauOExYYZRzS7Iz1pNw/u0m7HOKLj5ENdUB4zVG828YOauuwPGMVSulAXPU1UdzKpsVqKPej861OcSnLIVNJ3xTe1AF9JPMUVL1XBrNRypyCfpVuKYHgnFS0axmQSZSXPPWrcb/LUM67hkCiE/JRugTsxqdKcTxTEOBTjnFBmCdal7VGtPpAFGKO1FAxGwoqIDvUj802gQo4BNVXIL5qy5wlVcZNNCJFwRSjjAp6rgU1u1NgOcbo80kQ+XBqQfMtMClTSAimGCBT1w0RGBkdyabOeRTY+H7fjVdBdS5Z7SCCelWxs96oW+BKw7Y61dG3FZSOmndolDRgfdNKJAOi00AEcCk+boBUGth++RuAcU0oQeTTgrntTvKx95sGgE0RHA70xjk5zU2yP1OaTy8HgfnTTBiRgq4OKty3MIjLOCCB2qFRhucNiqNyxnbHQD0otdifuoSS8eYkLwKasEr/N1FPigHr2qwqlejVV0tjPlcnqyBYCDyKm+UD7tP3sOwNHmA/eUUm7lqNhBtzypoOztmn5jPqKcUjP8VSUQ4B70oXP8QNSeUp5DClMB7EUwuiLZ/nNO+YDjNPNs+M03ypOlId0GXA60ea/cUeW/vSFJOuDQLQcJV7pSmSLGNppmH6EU0g/3aBWXQG8onJzVa78sx/KDUzc9RUE4BjqkTNaGfnFKKSlrU5QPNJS9uaSgBDS5pKO1MRMsvy4OTToW5xUAGBTkbac8UrFKRKMClJB6U1+gpq0CuTL9acelMWpMVIxKd2pBS4+lAxhpAM0Hk0o45oAhmPao15NLIcsfrQg5qiScdKjf7wqTtUbcvSAlTpzSmkQcU6kMqz/AHhSEYOafOORSHpVolk0RG5WJJ9QB0rRVogBxmsqI/NjnHtV9CuBnJrORtS2LPmqPuqAaaZHPT+VMHXIFSBXbtxWZvZCbnI5bH40H3bNO8vH3iBTlKAdCTQVddBoDMeBSspBBLD86CzN7CmEZPXNAO48v5cRAAOe9MjEe3leaHPRdvT0p2V4wtF9Bct9QIjzlRilEKEZDgUhxS/IV70rjt2G+W46HP0pDuzyv5il5GMGnea2OeaYDOB2oyOtO84d1pcxnsRQK7I+OxpRkfxfrUhWID71NVFJ+8KB3Dc398/nSb3PG4/nTvJ54cUCA56igLoTLjuaN8o6E/jT2hdR1H50wxvgUCdmJ5kmetKZHHvSbHB6UuWXsTRcGkN809wPxFRzOjIcoOnpTyc87aifBB4qkS0rGTTu1JjnFL0rY5ApO1FGaBCUGiigApOhpaaeKAP/2Q",
                CitizenshipChecked = null
            };
        }
    }
}

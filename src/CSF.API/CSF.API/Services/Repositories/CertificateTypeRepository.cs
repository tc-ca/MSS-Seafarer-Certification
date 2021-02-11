namespace CSF.API.Services.Repositories
{
    using CSF.API.Data.Entities;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;

    public class CertificateTypeRepository : ICertificateTypeRepository
    {
        private IConfiguration configuration;

        public CertificateTypeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<CertificateType> GetAll()
        {
            return this.configuration.GetSection("Data:CertificateTypes").Get<List<CertificateType>>();
        }
    }
}

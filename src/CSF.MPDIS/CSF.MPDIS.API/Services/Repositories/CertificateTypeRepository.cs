namespace CSF.MPDIS.API.Services.Repositories
{
    using CSF.MPDIS.API.Data.Entities;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;

    public class CertificateTypeRepository : ICertificateTypeRepository
    {
        private IConfiguration configuration;

        public CertificateTypeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Add(CertificateType entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CertificateType> GetAll()
        {
            return this.configuration.GetSection("Data:CertificateTypes").Get<List<CertificateType>>();
        }
    }
}

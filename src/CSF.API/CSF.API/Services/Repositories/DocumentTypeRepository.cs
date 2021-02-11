namespace CSF.API.Services.Repositories
{
    using CSF.API.Data.Entities;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;

    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private IConfiguration configuration;

        public DocumentTypeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<DocumentType> GetAll()
        {
            return this.configuration.GetSection("Data:DocumentTypes").Get<List<DocumentType>>();
        }
    }
}

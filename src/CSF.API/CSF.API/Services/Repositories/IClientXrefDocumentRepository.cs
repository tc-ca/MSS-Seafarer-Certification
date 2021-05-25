using CSF.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.API.Services.Repositories
{
    public interface IClientXrefDocumentRepository
    {

        /// <summary>
        /// Function that returns the Document if found
        /// </summary>
        /// <param name="id">Id of the document you would like to find</param>
        /// <returns>Document object if found</returns>
        DocumentInfo GetDocument(Guid id);

        /// <summary>
        /// Returns multiple documents based off a list of IDs
        /// </summary>
        /// <param name="ids">List of IDs to find</param>
        /// <returns>List of documents</returns>
        IEnumerable<DocumentInfo> GetDocumentsByIds(Guid[] ids);

        IEnumerable<DocumentInfo> GetDocumentsByCdn(string cdn);

        DocumentInfo Insert(DocumentInfo document);

        DocumentInfo Update(DocumentInfo document);

        void Save();

    }
}

using CSF.API.Data.Contexts;
using CSF.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.API.Services.Repositories
{
    public class ClientXrefDocumentRepository : IClientXrefDocumentRepository
    {

        private readonly ClientDBContext context;

        public ClientXrefDocumentRepository(ClientDBContext context)
        {
            this.context = context;
        }

        public DocumentInfo GetDocument(Guid id)
        {
            try
            {
                var document = context.ClientXrefDocuments.Where(x => x.DocumentId == id)
                    .Select(x => new DocumentInfo
                    {
                        Cdn = x.CdnTxt,
                        DocumentId = x.DocumentId,
                        DateStartDte = x.DateStartDte.Value,
                        DateEndDte = x.DateEndDte ?? null
                    }).Single();

                return document;
            }
            catch (Exception exception)
            {

                throw;
            }
        }

        public IEnumerable<DocumentInfo> GetDocumentsByCdn(string cdn)
        {
            try
            {
                var documents = context.ClientXrefDocuments.Where(x => x.CdnTxt.Equals(cdn)).Select(x => new DocumentInfo
                {
                    Cdn = cdn,
                    DocumentId = x.DocumentId,
                    DateStartDte = x.DateStartDte.Value,
                    DateEndDte = x.DateEndDte ?? null
                }).ToList();

                return documents;

            }
            catch (Exception exception)
            {
                return new List<DocumentInfo>();
            }
        }

        public IEnumerable<DocumentInfo> GetDocumentsByIds(Guid[] ids)
        {
            try
            {

                var documents = context.ClientXrefDocuments.Where(x => ids.Contains(x.DocumentId))
                    .Select(x => new DocumentInfo
                    {
                        Cdn = x.CdnTxt,
                        DocumentId = x.DocumentId,
                        DateStartDte = x.DateStartDte.Value,
                        DateEndDte = x.DateEndDte ?? null
                    }).ToList();

                return documents;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public DocumentInfo Insert(DocumentInfo document)
        {
            try
            {
                var doc = new ClientXrefDocument
                {
                    CdnTxt = document.Cdn,
                    DocumentId = document.DocumentId,
                    DateStartDte = document.DateStartDte,
                    DateEndDte = document.DateEndDte
                };
                context.ClientXrefDocuments.Add(doc);

                context.SaveChanges();

                return document;

            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public DocumentInfo Update(DocumentInfo document)
        {
            try
            {
                var documentDB = context.ClientXrefDocuments.Where(x => x.CdnTxt == document.Cdn).Single();

                documentDB.DocumentId = document.DocumentId;
                documentDB.DateStartDte = document.DateStartDte;
                documentDB.DateEndDte = document.DateEndDte;

                context.ClientXrefDocuments.Update(documentDB);

                context.SaveChanges();

                return document;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}

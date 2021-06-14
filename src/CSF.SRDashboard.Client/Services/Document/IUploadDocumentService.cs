using CSF.SRDashboard.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CSF.SRDashboard.Client.Services.Document
{
    public interface IUploadDocumentService
    {
        [Inject]
        IDocumentService DocumentServe { get; set; }
        List<string> DocumentTypes { get; set; }
        string Language { get; }


        Task<List<Guid>> UploadDocument(UploadedDocument document);
    }
}
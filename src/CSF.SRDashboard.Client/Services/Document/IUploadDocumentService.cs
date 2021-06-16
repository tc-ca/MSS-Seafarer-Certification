using CSF.SRDashboard.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using CSF.SRDashboard.Client.Services.Document.Entities;

namespace CSF.SRDashboard.Client.Services.Document
{
    public interface IUploadDocumentService
    {
        [Inject]
        IDocumentService DocumentServe { get; set; }
        string Language { get; }


        Task<DocumentInfo> UploadDocument(UploadedDocument document);
    }
}
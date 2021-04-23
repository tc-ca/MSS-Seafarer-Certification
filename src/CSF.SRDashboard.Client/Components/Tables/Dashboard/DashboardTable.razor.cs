namespace CSF.SRDashboard.Client.Components.Tables.Dashboard
{
    using System;
    using System.Collections.Generic;
    using CSF.SRDashboard.Client.Models;
    using Microsoft.AspNetCore.Components;

    public partial class DashboardTable
    {
        [Parameter]
        public List<Document> TableData { get; set; } = new List<Document>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            TableData.Add(new Document
            {
                FileName = "mmeReport.pdf",
                Language = "English",
                RequestID = "123456",
                Type = "MME Report",
                DateUploaded = new DateTime(2020, 2, 10)
            });

            TableData.Add(new Document
            {
                FileName = "EyeExam.pdf",
                Language = "French",
                RequestID = "654321",
                Type = "Test results",
                DateUploaded = new DateTime(2019, 12, 14)
            });
        }
    }
}

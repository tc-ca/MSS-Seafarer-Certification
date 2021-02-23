using CSF.Components.Table.Entities;
using CSF.SRDashboard.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Utilities
{
    public class Utility
    {
        IMtoaArtifactService _artifactService;
        public Utility(IMtoaArtifactService artifactService)
        {
            _artifactService = artifactService;
        }
        public List<Row> FillDashboardRows( )
        {
            var dataRows = _artifactService.GetDashboardRowsInParallel();
            var dashboardRows = new List<Row>();

            var headerColumns = new List<Column>();
            headerColumns.Add(new Column() { Text = "" });
            headerColumns.Add(new Column() { Text = "Seafarer request #" });
            headerColumns.Add(new Column() { Text = "First and last name" });
            headerColumns.Add(new Column() { Text = "CDN" });
            headerColumns.Add(new Column() { Text = "Request type" });
            headerColumns.Add(new Column() { Text = "Assigned to" });
            headerColumns.Add(new Column() { Text = "Service standard" });
            headerColumns.Add(new Column() { Text = "Processing phase" });
            dashboardRows.Add(new Row()
            {
                Columns = headerColumns,
                IsHeaderRow = true,
                GroupId = 0
            });

            int counter = 0;
            foreach(var data in dataRows)
            {
                var columns = new List<Column>();
                columns.Add(new Column() { Text = "View", CssClass = "font-color-light-blue", Icon = "fa fa-eye", Link = string.Format("/requestdetails/{0}",data.ServiceRequestNumber) });
                columns.Add(new Column() { Text = data.ServiceRequestNumber.ToString() });
                columns.Add(new Column() { Text = "John Doe" });
                columns.Add(new Column() { Text = data.CDN });
                columns.Add(new Column() { Text = data.RequestType });
                columns.Add(new Column() { Text = data.AssignedTo });
                columns.Add(new Column() { Text = "60 days remaining" });
                columns.Add(new Column() { Text = data.ProcessingPhase });
                dashboardRows.Add(new Row()
                {
                    Columns = columns,
                    IsHeaderRow = false,
                    GroupId = counter
                });
                counter++;
            }

            return dashboardRows;
        }
    }
}

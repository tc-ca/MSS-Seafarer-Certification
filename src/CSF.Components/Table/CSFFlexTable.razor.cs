namespace CSF.Components.Table
{
    using CSF.Components.Table.Entities;
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;

    public partial class CSFFlexTable
    {
        public CSFFlexTable()
        {
            this.Rows = new List<Row>();
        }

        [Parameter]
        public List<Row> Rows { get; set; }
    }
}

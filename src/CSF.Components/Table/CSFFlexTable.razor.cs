namespace CSF.Components.Table
{
    using CSF.Components.Table.Entities;
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;

    public partial class CSFFlexTable
    {
        [Parameter]
        public List<Row> Rows { get; set; }
    }
}

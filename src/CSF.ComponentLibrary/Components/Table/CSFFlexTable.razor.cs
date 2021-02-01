using CSF.ComponentLibrary.Components.Table.Entities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace CSF.ComponentLibrary.Components.Table
{
    public partial class CSFFlexTable
    {
        [Parameter]
        public List<Row> Rows { get; set; }
    }
}

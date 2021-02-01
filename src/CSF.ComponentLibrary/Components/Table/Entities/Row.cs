using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.ComponentLibrary.Components.Table.Entities
{
    public class Row
    {
        public Row()
        {
            this.Columns = new List<Column>();
        }

        public int GroupId { get; set; }

        public bool IsHeaderRow { get; set; }

        public List<Column> Columns { get; set; }

        public void addColumn(Column column)
        {
            this.Columns.Add(column);
        }
    }
}

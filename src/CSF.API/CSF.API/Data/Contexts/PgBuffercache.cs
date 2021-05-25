using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CSF.API.Data.Contexts
{
    [Keyless]
    [Table("pg_buffercache")]
    public partial class PgBuffercache
    {
        [Column("bufferid")]
        public int? Bufferid { get; set; }
        [Column("relfilenode", TypeName = "oid")]
        public uint? Relfilenode { get; set; }
        [Column("reltablespace", TypeName = "oid")]
        public uint? Reltablespace { get; set; }
        [Column("reldatabase", TypeName = "oid")]
        public uint? Reldatabase { get; set; }
        [Column("relforknumber")]
        public short? Relforknumber { get; set; }
        [Column("relblocknumber")]
        public long? Relblocknumber { get; set; }
        [Column("isdirty")]
        public bool? Isdirty { get; set; }
        [Column("usagecount")]
        public short? Usagecount { get; set; }
        [Column("pinning_backends")]
        public int? PinningBackends { get; set; }
    }
}

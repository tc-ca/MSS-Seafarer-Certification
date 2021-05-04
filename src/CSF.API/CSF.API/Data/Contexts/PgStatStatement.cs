using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CSF.API.Data.Contexts
{
    [Keyless]
    [Table("pg_stat_statements")]
    public partial class PgStatStatement
    {
        [Column("userid", TypeName = "oid")]
        public uint? Userid { get; set; }
        [Column("dbid", TypeName = "oid")]
        public uint? Dbid { get; set; }
        [Column("queryid")]
        public long? Queryid { get; set; }
        [Column("query")]
        public string Query { get; set; }
        [Column("calls")]
        public long? Calls { get; set; }
        [Column("total_time")]
        public double? TotalTime { get; set; }
        [Column("min_time")]
        public double? MinTime { get; set; }
        [Column("max_time")]
        public double? MaxTime { get; set; }
        [Column("mean_time")]
        public double? MeanTime { get; set; }
        [Column("stddev_time")]
        public double? StddevTime { get; set; }
        [Column("rows")]
        public long? Rows { get; set; }
        [Column("shared_blks_hit")]
        public long? SharedBlksHit { get; set; }
        [Column("shared_blks_read")]
        public long? SharedBlksRead { get; set; }
        [Column("shared_blks_dirtied")]
        public long? SharedBlksDirtied { get; set; }
        [Column("shared_blks_written")]
        public long? SharedBlksWritten { get; set; }
        [Column("local_blks_hit")]
        public long? LocalBlksHit { get; set; }
        [Column("local_blks_read")]
        public long? LocalBlksRead { get; set; }
        [Column("local_blks_dirtied")]
        public long? LocalBlksDirtied { get; set; }
        [Column("local_blks_written")]
        public long? LocalBlksWritten { get; set; }
        [Column("temp_blks_read")]
        public long? TempBlksRead { get; set; }
        [Column("temp_blks_written")]
        public long? TempBlksWritten { get; set; }
        [Column("blk_read_time")]
        public double? BlkReadTime { get; set; }
        [Column("blk_write_time")]
        public double? BlkWriteTime { get; set; }
    }
}

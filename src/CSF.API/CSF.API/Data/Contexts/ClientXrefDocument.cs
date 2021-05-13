using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CSF.API.Data.Contexts
{
    [Table("client_xref_document")]
    public partial class ClientXrefDocument
    {
        [Key]
        [Column("cdn_txt")]
        public string CdnTxt { get; set; }
        [Key]
        [Column("document_id")]
        public Guid DocumentId { get; set; }
        [Column("date_start_dte", TypeName = "date")]
        public DateTime? DateStartDte { get; set; }
        [Column("date_end_dte", TypeName = "date")]
        public DateTime? DateEndDte { get; set; }
    }
}

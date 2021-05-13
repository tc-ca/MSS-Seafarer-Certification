using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CSF.API.Data.Contexts
{
    public partial class ClientDBContext : DbContext
    {

        public ClientDBContext(DbContextOptions<ClientDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClientXrefDocument> ClientXrefDocuments { get; set; }
        public virtual DbSet<PgBuffercache> PgBuffercaches { get; set; }
        public virtual DbSet<PgStatStatement> PgStatStatements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_buffercache")
                .HasPostgresExtension("pg_stat_statements")
                .HasPostgresExtension("uuid-ossp")
                .HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<ClientXrefDocument>(entity =>
            {
                entity.HasKey(e => new { e.CdnTxt, e.DocumentId })
                    .HasName("client_xref_document_pkey");

                entity.HasComment("Table linking clients to all related documents from the Document Storage service.");

                entity.HasIndex(e => e.CdnTxt, "client_id_idx")
                    .HasMethod("hash")
                    .HasOperators(new[] { "varchar_ops" })
                    .UseCollation(new[] { ".utf8" });

                entity.Property(e => e.CdnTxt).HasComment("The unique identifier of the client.");

                entity.Property(e => e.DocumentId)
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .HasComment("The unique identifier of the document from the Document Storage service.");

                entity.Property(e => e.DateEndDte).HasComment("The date marking the end of the relationship between the client and the document. A future date represents an active relationship.");

                entity.Property(e => e.DateStartDte).HasComment("The date from which the link from the client to the document is valid.");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

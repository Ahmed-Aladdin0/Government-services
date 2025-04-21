using Government.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Government.Data.DataBaseConfigurations
{
    public class AttachedDocumentConfig : IEntityTypeConfiguration<AttachedDocument>
    {

        public void Configure(EntityTypeBuilder<AttachedDocument> builder)
        {

            builder.Property(x => x.FileName).HasMaxLength(250);
            builder.Property(x => x.ContentType).HasMaxLength(50);
            builder.Property(x => x.FileExtension).HasMaxLength(10);

            builder.ToTable("AttachedDocuments");

        }
    }
}

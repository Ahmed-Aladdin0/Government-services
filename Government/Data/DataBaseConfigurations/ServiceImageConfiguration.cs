using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Government.Data.DataBaseConfigurations
{
    public class ServiceImageConfiguration : IEntityTypeConfiguration<ServiceImage>
    {
        public void Configure(EntityTypeBuilder<ServiceImage> builder)
        {
            builder.Property(x => x.ImageName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.ContentType)
                .HasMaxLength(50);


            builder.Property(x => x.ImageExtension).HasMaxLength(10);

            builder.ToTable("ServiceImages");
        }
    }
}

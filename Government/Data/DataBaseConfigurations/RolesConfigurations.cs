using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurvayBasket.Abstractions.Consts.cs;

namespace Government.Data.DataBaseConfigurations
{
    public class RolesConfigurations : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(new AppRole
            {
                Id = DefaultRole.AdminRoleId,
                Name = DefaultRole.Admin,
                NormalizedName = DefaultRole.Admin.ToUpper(),
                ConcurrencyStamp = DefaultRole.AdminConcurrencyStamp,
            }



                );
        }
    }
}

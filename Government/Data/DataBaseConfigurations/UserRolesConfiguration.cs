using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurvayBasket.Abstractions.Consts.cs;

namespace Government.Data.DataBaseConfigurations
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>
            {

                UserId = DefaultUser.AdminId,
                RoleId = DefaultRole.AdminRoleId

            });

        }
    }
}

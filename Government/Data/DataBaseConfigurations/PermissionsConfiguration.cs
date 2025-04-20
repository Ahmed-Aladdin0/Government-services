using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurvayBasket.Abstractions.Consts.cs;

namespace Government.Data.DataBaseConfigurations
{
    public class PermissionsConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            var permissions = Permissions.GetAllPermissions();

            var AdminClaims = new List<IdentityRoleClaim<string>>();

            for (int i = 0; i < permissions.Count; i++)
            {
                AdminClaims.Add(new IdentityRoleClaim<string>
                {

                    Id = i + 1,
                    ClaimType = Permissions.Type,
                    ClaimValue = permissions[i],
                    RoleId = DefaultRole.AdminRoleId
                });
            }

            builder.HasData(AdminClaims);
        }
    }
}

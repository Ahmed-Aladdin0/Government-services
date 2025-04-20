using Government.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurvayBasket.Abstractions.Consts.cs;

namespace Government.Data.DataBaseConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            var Hasher = new PasswordHasher<AppUser>();
            builder.HasData(new AppUser
            {

                Id = DefaultUser.AdminId,
                FirstName = "Government_Services",
                LastName = "Admin",
                Email = DefaultUser.AdminEmail,
                NormalizedEmail = DefaultUser.AdminEmail.ToUpper(),
                UserName = DefaultUser.AdminEmail,
                NormalizedUserName = DefaultUser.AdminEmail.ToUpper(),
                ConcurrencyStamp = DefaultUser.AdminConcurrencyStamp,
                SecurityStamp = DefaultUser.AdminSecurityStamp,
                PasswordHash = Hasher.HashPassword(null!, DefaultUser.AdminPassword),
                EmailConfirmed = true,


            });



        }
    }
}

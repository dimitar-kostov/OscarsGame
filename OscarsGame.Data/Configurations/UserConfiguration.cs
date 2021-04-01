using OscarsGame.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OscarsGame.Data.Configurations
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        internal UserConfiguration()
        {
            HasKey(x => x.UserId)
                .Property(x => x.UserId)
                .IsRequired();

            Property(x => x.PasswordHash);

            Property(x => x.SecurityStamp);

            Property(x => x.UserName)
                .HasMaxLength(256)
                .IsRequired();

            Property(x => x.DisplayName)
                .HasMaxLength(256)
                .IsRequired();

            Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();

            Property(x => x.EmailConfirmed)
                .IsRequired();

            HasMany(x => x.Claims)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId);

            HasMany(x => x.Logins)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId);

            HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}

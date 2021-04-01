using OscarsGame.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OscarsGame.Data.Configurations
{
    internal class ExternalLoginConfiguration : EntityTypeConfiguration<ExternalLogin>
    {
        internal ExternalLoginConfiguration()
        {
            HasKey(x => new { x.LoginProvider, x.ProviderKey, x.UserId });

            Property(x => x.LoginProvider)
                .HasMaxLength(128)
                .IsRequired();

            Property(x => x.ProviderKey)
                .HasMaxLength(128)
                .IsRequired();

            Property(x => x.UserId)
                .IsRequired();

            HasRequired(x => x.User)
                .WithMany(x => x.Logins)
                .HasForeignKey(x => x.UserId);
        }
    }
}

using OscarsGame.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OscarsGame.Data.Configurations
{
    internal class ClaimConfiguration : EntityTypeConfiguration<Claim>
    {
        internal ClaimConfiguration()
        {
            HasKey(x => x.ClaimId)
                .Property(x => x.ClaimId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.UserId)
                .IsRequired();

            Property(x => x.ClaimType);

            Property(x => x.ClaimValue);

            HasRequired(x => x.User)
                .WithMany(x => x.Claims)
                .HasForeignKey(x => x.UserId);
        }
    }
}

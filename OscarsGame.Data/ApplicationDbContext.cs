using OscarsGame.Data.Configurations;
using OscarsGame.Data.Migrations;
using OscarsGame.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace OscarsGame.Data
{
    public class ApplicationDbContext : DbContext
    {
        internal ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ApplicationDbContext, MigrationsConfiguration>(
                    useSuppliedContext: true));
        }

        public IDbSet<Movie> Movies { get; set; }
        public IDbSet<Category> Caterogries { get; set; }
        public IDbSet<Watched> Watched { get; set; }
        public IDbSet<Bet> Bets { get; set; }
        public IDbSet<GameProperties> Game { get; set; }
        public IDbSet<MovieCredit> Credits { get; set; }
        public IDbSet<Nomination> Nominations { get; set; }

        internal IDbSet<User> Users { get; set; }
        internal IDbSet<ExternalLogin> Logins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<MovieCredit>()
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new ExternalLoginConfiguration());
            modelBuilder.Configurations.Add(new ClaimConfiguration());
        }
    }
}

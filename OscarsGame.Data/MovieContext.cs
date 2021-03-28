using OscarsGame.Data.Migrations;
using OscarsGame.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace OscarsGame.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MovieContext, Configuration>("DefaultConnection"));
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Caterogries { get; set; }
        public DbSet<Watched> Watched { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<GameProperties> Game { get; set; }
        public DbSet<MovieCredit> Credits { get; set; }
        public DbSet<Nomination> Nominations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<MovieCredit>()
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
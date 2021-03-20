namespace OscarsGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Category_Id = c.Int(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryTtle = c.String(),
                        CategoryDescription = c.String(),
                        Winner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Winner_Id)
                .Index(t => t.Winner_Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Title = c.String(),
                        ReleaseDate = c.String(),
                        PosterPath = c.String(),
                        Overview = c.String(),
                        ImdbId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Watcheds",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.GameProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StopGameDate = c.DateTime(nullable: false),
                        StartGameDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieCategory",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.CategoryId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.WatchedMovies",
                c => new
                    {
                        Watched_UserId = c.String(nullable: false, maxLength: 128),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Watched_UserId, t.Movie_Id })
                .ForeignKey("dbo.Watcheds", t => t.Watched_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Watched_UserId)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "Winner_Id", "dbo.Movies");
            DropForeignKey("dbo.WatchedMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.WatchedMovies", "Watched_UserId", "dbo.Watcheds");
            DropForeignKey("dbo.MovieCategory", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.MovieCategory", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Bets", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Bets", "Category_Id", "dbo.Categories");
            DropIndex("dbo.WatchedMovies", new[] { "Movie_Id" });
            DropIndex("dbo.WatchedMovies", new[] { "Watched_UserId" });
            DropIndex("dbo.MovieCategory", new[] { "CategoryId" });
            DropIndex("dbo.MovieCategory", new[] { "MovieId" });
            DropIndex("dbo.Categories", new[] { "Winner_Id" });
            DropIndex("dbo.Bets", new[] { "Movie_Id" });
            DropIndex("dbo.Bets", new[] { "Category_Id" });
            DropTable("dbo.WatchedMovies");
            DropTable("dbo.MovieCategory");
            DropTable("dbo.GameProperties");
            DropTable("dbo.Watcheds");
            DropTable("dbo.Movies");
            DropTable("dbo.Categories");
            DropTable("dbo.Bets");
        }
    }
}

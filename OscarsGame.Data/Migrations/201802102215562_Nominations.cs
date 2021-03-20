namespace OscarsGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nominations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bets", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Bets", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.MovieCategory", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieCategory", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Winner_Id", "dbo.Movies");
            DropIndex("dbo.Bets", new[] { "Category_Id" });
            DropIndex("dbo.Bets", new[] { "Movie_Id" });
            DropIndex("dbo.Categories", new[] { "Winner_Id" });
            DropIndex("dbo.MovieCategory", new[] { "MovieId" });
            DropIndex("dbo.MovieCategory", new[] { "CategoryId" });
            CreateTable(
                "dbo.Nominations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsWinner = c.Boolean(nullable: false),
                        Category_Id = c.Int(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.MovieCredits",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Order = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        Name = c.String(),
                        IsCast = c.Boolean(nullable: false),
                        Role = c.String(),
                        ProfilePath = c.String(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.MovieCreditNominations",
                c => new
                    {
                        MovieCredit_Id = c.String(nullable: false, maxLength: 128),
                        Nomination_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieCredit_Id, t.Nomination_Id })
                .ForeignKey("dbo.MovieCredits", t => t.MovieCredit_Id, cascadeDelete: true)
                .ForeignKey("dbo.Nominations", t => t.Nomination_Id, cascadeDelete: true)
                .Index(t => t.MovieCredit_Id)
                .Index(t => t.Nomination_Id);
            
            AddColumn("dbo.Bets", "Nomination_Id", c => c.Int());
            CreateIndex("dbo.Bets", "Nomination_Id");
            AddForeignKey("dbo.Bets", "Nomination_Id", "dbo.Nominations", "Id");
            DropColumn("dbo.Bets", "Category_Id");
            DropColumn("dbo.Bets", "Movie_Id");
            DropColumn("dbo.Categories", "Winner_Id");
            DropTable("dbo.MovieCategory");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MovieCategory",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.CategoryId });
            
            AddColumn("dbo.Categories", "Winner_Id", c => c.Int());
            AddColumn("dbo.Bets", "Movie_Id", c => c.Int());
            AddColumn("dbo.Bets", "Category_Id", c => c.Int());
            DropForeignKey("dbo.MovieCreditNominations", "Nomination_Id", "dbo.Nominations");
            DropForeignKey("dbo.MovieCreditNominations", "MovieCredit_Id", "dbo.MovieCredits");
            DropForeignKey("dbo.Nominations", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.MovieCredits", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Nominations", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Bets", "Nomination_Id", "dbo.Nominations");
            DropIndex("dbo.MovieCreditNominations", new[] { "Nomination_Id" });
            DropIndex("dbo.MovieCreditNominations", new[] { "MovieCredit_Id" });
            DropIndex("dbo.MovieCredits", new[] { "Movie_Id" });
            DropIndex("dbo.Nominations", new[] { "Movie_Id" });
            DropIndex("dbo.Nominations", new[] { "Category_Id" });
            DropIndex("dbo.Bets", new[] { "Nomination_Id" });
            DropColumn("dbo.Bets", "Nomination_Id");
            DropTable("dbo.MovieCreditNominations");
            DropTable("dbo.MovieCredits");
            DropTable("dbo.Nominations");
            CreateIndex("dbo.MovieCategory", "CategoryId");
            CreateIndex("dbo.MovieCategory", "MovieId");
            CreateIndex("dbo.Categories", "Winner_Id");
            CreateIndex("dbo.Bets", "Movie_Id");
            CreateIndex("dbo.Bets", "Category_Id");
            AddForeignKey("dbo.Categories", "Winner_Id", "dbo.Movies", "Id");
            AddForeignKey("dbo.MovieCategory", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieCategory", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bets", "Movie_Id", "dbo.Movies", "Id");
            AddForeignKey("dbo.Bets", "Category_Id", "dbo.Categories", "Id");
        }
    }
}

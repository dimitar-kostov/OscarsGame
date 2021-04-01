namespace OscarsGame.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Link_User : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WatchedMovies", "Watched_UserId", "dbo.Watcheds");
            DropIndex("dbo.WatchedMovies", new[] { "Watched_UserId" });
            RenameColumn(table: "dbo.WatchedMovies", name: "Watched_UserId", newName: "Watched_Id");
            DropPrimaryKey("dbo.Watcheds");
            DropPrimaryKey("dbo.WatchedMovies");
            AddColumn("dbo.Watcheds", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Bets", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Watcheds", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.WatchedMovies", "Watched_Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Watcheds", "Id");
            AddPrimaryKey("dbo.WatchedMovies", new[] { "Watched_Id", "Movie_Id" });
            CreateIndex("dbo.Bets", "UserId");
            CreateIndex("dbo.Watcheds", "UserId");
            CreateIndex("dbo.WatchedMovies", "Watched_Id");
            AddForeignKey("dbo.Watcheds", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Bets", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.WatchedMovies", "Watched_Id", "dbo.Watcheds", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.WatchedMovies", "Watched_Id", "dbo.Watcheds");
            DropForeignKey("dbo.Bets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Watcheds", "UserId", "dbo.Users");
            DropIndex("dbo.WatchedMovies", new[] { "Watched_Id" });
            DropIndex("dbo.Watcheds", new[] { "UserId" });
            DropIndex("dbo.Bets", new[] { "UserId" });
            DropPrimaryKey("dbo.WatchedMovies");
            DropPrimaryKey("dbo.Watcheds");
            AlterColumn("dbo.WatchedMovies", "Watched_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Watcheds", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Bets", "UserId", c => c.String());
            DropColumn("dbo.Watcheds", "Id");
            AddPrimaryKey("dbo.WatchedMovies", new[] { "Watched_UserId", "Movie_Id" });
            AddPrimaryKey("dbo.Watcheds", "UserId");
            RenameColumn(table: "dbo.WatchedMovies", name: "Watched_Id", newName: "Watched_UserId");
            CreateIndex("dbo.WatchedMovies", "Watched_UserId");
            AddForeignKey("dbo.WatchedMovies", "Watched_UserId", "dbo.Watcheds", "UserId", cascadeDelete: true);
        }
    }
}

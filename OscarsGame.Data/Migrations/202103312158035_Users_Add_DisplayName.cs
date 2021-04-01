namespace OscarsGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users_Add_DisplayName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DisplayName", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DisplayName");
        }
    }
}

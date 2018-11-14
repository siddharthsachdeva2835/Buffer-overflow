namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class token : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        TokenID = c.Int(nullable: false),
                        TokenString = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TokenID)
                .ForeignKey("dbo.User", t => t.TokenID)
                .Index(t => t.TokenID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Token", "TokenID", "dbo.User");
            DropIndex("dbo.Token", new[] { "TokenID" });
            DropTable("dbo.Token");
        }
    }
}

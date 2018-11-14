namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        QuestionID = c.Int(nullable: false),
                        Author_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.User", t => t.Author_UserID)
                .ForeignKey("dbo.Question", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID)
                .Index(t => t.Author_UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        EmailID = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Voting",
                c => new
                    {
                        AnswerID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnswerID, t.UserID })
                .ForeignKey("dbo.Answer", t => t.AnswerID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.AnswerID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        AnswerCount = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Author_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.User", t => t.Author_UserID)
                .Index(t => t.Author_UserID);
            
            CreateTable(
                "dbo.QuestionTag",
                c => new
                    {
                        QuestionID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionID, t.TagID })
                .ForeignKey("dbo.Question", t => t.QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagID, cascadeDelete: true)
                .Index(t => t.QuestionID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                    })
                .PrimaryKey(t => t.TagID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionTag", "TagID", "dbo.Tag");
            DropForeignKey("dbo.QuestionTag", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.Question", "Author_UserID", "dbo.User");
            DropForeignKey("dbo.Answer", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.Answer", "Author_UserID", "dbo.User");
            DropForeignKey("dbo.Voting", "UserID", "dbo.User");
            DropForeignKey("dbo.Voting", "AnswerID", "dbo.Answer");
            DropIndex("dbo.QuestionTag", new[] { "TagID" });
            DropIndex("dbo.QuestionTag", new[] { "QuestionID" });
            DropIndex("dbo.Question", new[] { "Author_UserID" });
            DropIndex("dbo.Voting", new[] { "UserID" });
            DropIndex("dbo.Voting", new[] { "AnswerID" });
            DropIndex("dbo.Answer", new[] { "Author_UserID" });
            DropIndex("dbo.Answer", new[] { "QuestionID" });
            DropTable("dbo.Tag");
            DropTable("dbo.QuestionTag");
            DropTable("dbo.Question");
            DropTable("dbo.Voting");
            DropTable("dbo.User");
            DropTable("dbo.Answer");
        }
    }
}

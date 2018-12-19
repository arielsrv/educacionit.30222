namespace PhotoSharing.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Subject = c.String(nullable: false, maxLength: 250),
                        Body = c.String(),
                        PhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photos", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.PhotoId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        PhotoFile = c.Binary(),
                        ImageMimeType = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PhotoId", "dbo.Photos");
            DropIndex("dbo.Comments", new[] { "PhotoId" });
            DropTable("dbo.Photos");
            DropTable("dbo.Comments");
        }
    }
}

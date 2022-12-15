namespace SiteOfMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mignewloock : DbMigration
    {
        
        public override void Up()
        {
            CreateTable(
                "dbo.Quoters",
                c => new
                    {
                        QuoterId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Thumb = c.String(),
                    })
                .PrimaryKey(t => t.QuoterId);

            this.Sql("DELETE FROM dbo.Quotes");

            AddColumn("dbo.Posts", "Thumb", c => c.String());
            AddColumn("dbo.Tags", "Thumb", c => c.String());
            AddColumn("dbo.Quotes", "QuoterId", c => c.Int(nullable: false));
            AlterColumn("dbo.AnonymousUsers", "Name", c => c.String(defaultValue:"ناشناس"));
            AddForeignKey("dbo.RelatedLinks", "PostId", "dbo.Posts", "PostId", cascadeDelete: true);
            AddForeignKey("dbo.Quotes", "QuoterId", "dbo.Quoters", "QuoterId", cascadeDelete: true);
            CreateIndex("dbo.RelatedLinks", "PostId");
            CreateIndex("dbo.Quotes", "QuoterId");
            DropColumn("dbo.Tags", "alk");
            DropColumn("dbo.Quotes", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotes", "Author", c => c.String(nullable: false));
            AddColumn("dbo.Tags", "alk", c => c.String());
            DropIndex("dbo.Quotes", new[] { "QuoterId" });
            DropIndex("dbo.RelatedLinks", new[] { "PostId" });
            DropForeignKey("dbo.Quotes", "QuoterId", "dbo.Quoters");
            DropForeignKey("dbo.RelatedLinks", "PostId", "dbo.Posts");
            AlterColumn("dbo.AnonymousUsers", "Name", c => c.String());
            DropColumn("dbo.Quotes", "QuoterId");
            DropColumn("dbo.Tags", "Thumb");
            DropColumn("dbo.Posts", "Thumb");
            DropTable("dbo.Quoters");
        }
    }
}

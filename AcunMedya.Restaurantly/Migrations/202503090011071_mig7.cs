namespace AcunMedya.Restaurantly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NavbarViewModels",
                c => new
                    {
                        NavbarViewModelId = c.Int(nullable: false, identity: true),
                        UnreadMessageCount = c.Int(nullable: false),
                        UnreadNotificationCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NavbarViewModelId);
            
            CreateTable(
                "dbo.SocialMedias",
                c => new
                    {
                        SocialMediaId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.SocialMediaId);
            
            AddColumn("dbo.Contacts", "NavbarViewModel_NavbarViewModelId", c => c.Int());
            AddColumn("dbo.Notifications", "NavbarViewModel_NavbarViewModelId", c => c.Int());
            CreateIndex("dbo.Contacts", "NavbarViewModel_NavbarViewModelId");
            CreateIndex("dbo.Notifications", "NavbarViewModel_NavbarViewModelId");
            AddForeignKey("dbo.Notifications", "NavbarViewModel_NavbarViewModelId", "dbo.NavbarViewModels", "NavbarViewModelId");
            AddForeignKey("dbo.Contacts", "NavbarViewModel_NavbarViewModelId", "dbo.NavbarViewModels", "NavbarViewModelId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "NavbarViewModel_NavbarViewModelId", "dbo.NavbarViewModels");
            DropForeignKey("dbo.Notifications", "NavbarViewModel_NavbarViewModelId", "dbo.NavbarViewModels");
            DropIndex("dbo.Notifications", new[] { "NavbarViewModel_NavbarViewModelId" });
            DropIndex("dbo.Contacts", new[] { "NavbarViewModel_NavbarViewModelId" });
            DropColumn("dbo.Notifications", "NavbarViewModel_NavbarViewModelId");
            DropColumn("dbo.Contacts", "NavbarViewModel_NavbarViewModelId");
            DropTable("dbo.SocialMedias");
            DropTable("dbo.NavbarViewModels");
        }
    }
}

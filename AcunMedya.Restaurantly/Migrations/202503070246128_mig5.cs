namespace AcunMedya.Restaurantly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galeries",
                c => new
                    {
                        GaleryId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.GaleryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Galeries");
        }
    }
}

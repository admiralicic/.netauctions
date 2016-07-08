namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_KomentariArtikla : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KomentariArtikala",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        TextKomentara = c.String(),
                        KomentatorId = c.Int(nullable: false),
                        ArtikalId = c.Int(nullable: false),
                        AukcijaId = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artikli", t => t.ArtikalId)
                .ForeignKey("dbo.AspNetUsers", t => t.KomentatorId)
                .Index(t => t.KomentatorId)
                .Index(t => t.ArtikalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KomentariArtikala", "KomentatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.KomentariArtikala", "ArtikalId", "dbo.Artikli");
            DropIndex("dbo.KomentariArtikala", new[] { "ArtikalId" });
            DropIndex("dbo.KomentariArtikala", new[] { "KomentatorId" });
            DropTable("dbo.KomentariArtikala");
        }
    }
}

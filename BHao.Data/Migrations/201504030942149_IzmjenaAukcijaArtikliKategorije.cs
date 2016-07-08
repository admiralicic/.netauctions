namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IzmjenaAukcijaArtikliKategorije : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Artikli", "KategorijaId", "dbo.Kategorije");
            DropForeignKey("dbo.Slike", "ArtikalId", "dbo.Artikli");
            DropIndex("dbo.Artikli", new[] { "KategorijaId" });
            DropIndex("dbo.Slike", new[] { "ArtikalId" });
            AddColumn("dbo.Slike", "SlikaIme", c => c.String());
            AddColumn("dbo.Slike", "SlikaThumbIme", c => c.String());
            AddColumn("dbo.Slike", "AukcijaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Slike", "AukcijaId");
            AddForeignKey("dbo.Slike", "AukcijaId", "dbo.Aukcije", "Id");
            DropColumn("dbo.Artikli", "KategorijaId");
            DropColumn("dbo.Slike", "SlikaThumb");
            DropColumn("dbo.Slike", "SlikaAdd");
            DropColumn("dbo.Slike", "Opis");
            DropColumn("dbo.Slike", "SlikaData");
            DropColumn("dbo.Slike", "SlikaDataMimeType");
            DropColumn("dbo.Slike", "SlikaThumbData");
            DropColumn("dbo.Slike", "SlikaDataThumbMimeType");
            DropColumn("dbo.Slike", "ArtikalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slike", "ArtikalId", c => c.Int(nullable: false));
            AddColumn("dbo.Slike", "SlikaDataThumbMimeType", c => c.String());
            AddColumn("dbo.Slike", "SlikaThumbData", c => c.Binary());
            AddColumn("dbo.Slike", "SlikaDataMimeType", c => c.String());
            AddColumn("dbo.Slike", "SlikaData", c => c.Binary());
            AddColumn("dbo.Slike", "Opis", c => c.String());
            AddColumn("dbo.Slike", "SlikaAdd", c => c.String());
            AddColumn("dbo.Slike", "SlikaThumb", c => c.String());
            AddColumn("dbo.Artikli", "KategorijaId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Slike", "AukcijaId", "dbo.Aukcije");
            DropIndex("dbo.Slike", new[] { "AukcijaId" });
            DropColumn("dbo.Slike", "AukcijaId");
            DropColumn("dbo.Slike", "SlikaThumbIme");
            DropColumn("dbo.Slike", "SlikaIme");
            CreateIndex("dbo.Slike", "ArtikalId");
            CreateIndex("dbo.Artikli", "KategorijaId");
            AddForeignKey("dbo.Slike", "ArtikalId", "dbo.Artikli", "Id");
            AddForeignKey("dbo.Artikli", "KategorijaId", "dbo.Kategorije", "Id");
        }
    }
}

namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityBase_add_isDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artikli", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Aukcije", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Kategorije", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Gradovi", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.NaciniPlacanja", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ponude", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Slike", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Inkrementi", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.KomentariKorisnika", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.OcjeneArtikala", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.OcjeneKorisnika", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Poruke", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Poruke", "isDeleted");
            DropColumn("dbo.OcjeneKorisnika", "isDeleted");
            DropColumn("dbo.OcjeneArtikala", "isDeleted");
            DropColumn("dbo.KomentariKorisnika", "isDeleted");
            DropColumn("dbo.Inkrementi", "isDeleted");
            DropColumn("dbo.Slike", "isDeleted");
            DropColumn("dbo.Ponude", "isDeleted");
            DropColumn("dbo.NaciniPlacanja", "isDeleted");
            DropColumn("dbo.Gradovi", "isDeleted");
            DropColumn("dbo.Kategorije", "isDeleted");
            DropColumn("dbo.Aukcije", "isDeleted");
            DropColumn("dbo.Artikli", "isDeleted");
        }
    }
}

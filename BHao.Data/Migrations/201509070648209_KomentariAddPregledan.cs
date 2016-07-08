namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KomentariAddPregledan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KomentariArtikala", "KomentarPregledan", c => c.Boolean(nullable: false));
            AddColumn("dbo.KomentariKorisnika", "KomentarPregledan", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KomentariKorisnika", "KomentarPregledan");
            DropColumn("dbo.KomentariArtikala", "KomentarPregledan");
        }
    }
}

namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OcjenaArtikla_add_ArtikalId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OcjeneArtikala", "AukcijaId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OcjeneArtikala", "AukcijaId");
        }
    }
}

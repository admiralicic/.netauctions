namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OcjenaKorisnikaAddedAukcijaId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OcjeneKorisnika", "AukcijaId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OcjeneKorisnika", "AukcijaId");
        }
    }
}

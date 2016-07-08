namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KomentarKorisnika_add_AukcijaId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KomentariKorisnika", "AukcijaId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KomentariKorisnika", "AukcijaId");
        }
    }
}

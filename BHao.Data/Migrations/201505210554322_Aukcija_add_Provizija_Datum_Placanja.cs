namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aukcija_add_Provizija_Datum_Placanja : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Aukcije", "Provizija", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Aukcije", "DatumPlacanja", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Aukcije", "DatumPlacanja");
            DropColumn("dbo.Aukcije", "Provizija");
        }
    }
}

namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aukcija_DatumPlacanja_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Aukcije", "DatumPlacanja", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Aukcije", "DatumPlacanja", c => c.DateTime(nullable: false));
        }
    }
}

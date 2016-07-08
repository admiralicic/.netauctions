namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AukcijaAddNajveciPonudjacField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Aukcije", "NajveciPonudjacId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Aukcije", "NajveciPonudjacId");
        }
    }
}

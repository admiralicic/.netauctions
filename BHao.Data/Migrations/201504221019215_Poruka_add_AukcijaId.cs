namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Poruka_add_AukcijaId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Poruke", "AukcijaId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Poruke", "AukcijaId");
        }
    }
}

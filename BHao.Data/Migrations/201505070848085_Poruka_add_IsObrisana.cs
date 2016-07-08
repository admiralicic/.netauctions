namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Poruka_add_IsObrisana : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Poruke", "IsObrisana", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Poruke", "IsObrisana");
        }
    }
}

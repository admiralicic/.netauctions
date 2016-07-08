namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Poruka_add_Obrisana_za_primalac_i_posiljalac : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Poruke", "IsObrisanaPosiljalac", c => c.Boolean(nullable: false));
            AddColumn("dbo.Poruke", "IsObrisanaPrimalac", c => c.Boolean(nullable: false));
            DropColumn("dbo.Poruke", "IsObrisana");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Poruke", "IsObrisana", c => c.Boolean(nullable: false));
            DropColumn("dbo.Poruke", "IsObrisanaPrimalac");
            DropColumn("dbo.Poruke", "IsObrisanaPosiljalac");
        }
    }
}

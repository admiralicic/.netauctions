namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Grad_promjena_Ime_u_Naziv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gradovi", "Naziv", c => c.String());
            DropColumn("dbo.Gradovi", "Ime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gradovi", "Ime", c => c.String());
            DropColumn("dbo.Gradovi", "Naziv");
        }
    }
}

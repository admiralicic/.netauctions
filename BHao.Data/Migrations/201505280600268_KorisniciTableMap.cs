namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KorisniciTableMap : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "Korisnici");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Korisnici", newName: "AspNetUsers");
        }
    }
}

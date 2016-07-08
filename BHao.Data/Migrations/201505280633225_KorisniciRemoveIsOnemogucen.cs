namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KorisniciRemoveIsOnemogucen : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Korisnici", newName: "AspNetUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "Korisnici");
        }
    }
}

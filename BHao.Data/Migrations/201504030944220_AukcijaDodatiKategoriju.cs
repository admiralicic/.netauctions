namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AukcijaDodatiKategoriju : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Aukcije", "KategorijaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Aukcije", "KategorijaId");
            AddForeignKey("dbo.Aukcije", "KategorijaId", "dbo.Kategorije", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Aukcije", "KategorijaId", "dbo.Kategorije");
            DropIndex("dbo.Aukcije", new[] { "KategorijaId" });
            DropColumn("dbo.Aukcije", "KategorijaId");
        }
    }
}

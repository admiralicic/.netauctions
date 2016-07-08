namespace BHao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artikli",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Proizvodjac = c.String(),
                        Model = c.String(),
                        Naziv = c.String(),
                        KategorijaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategorije", t => t.KategorijaId)
                .Index(t => t.KategorijaId);
            
            CreateTable(
                "dbo.Kategorije",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Slike",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SlikaThumb = c.String(),
                        SlikaAdd = c.String(),
                        Opis = c.String(),
                        SlikaData = c.Binary(),
                        SlikaDataMimeType = c.String(),
                        SlikaThumbData = c.Binary(),
                        SlikaDataThumbMimeType = c.String(),
                        ArtikalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artikli", t => t.ArtikalId)
                .Index(t => t.ArtikalId);
            
            CreateTable(
                "dbo.Aukcije",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pocetak = c.DateTime(nullable: false),
                        Zavrsetak = c.DateTime(nullable: false),
                        MinimalnaCijena = c.Decimal(precision: 18, scale: 2),
                        KupiOdmahCijena = c.Decimal(precision: 18, scale: 2),
                        Napomena = c.String(),
                        Naziv = c.String(),
                        DetaljanOpis = c.String(),
                        NajvecaPonuda = c.Decimal(precision: 18, scale: 2),
                        Zavrsena = c.Boolean(nullable: false),
                        ProdavacId = c.Int(nullable: false),
                        KupacId = c.Int(),
                        ArtikalId = c.Int(),
                        NacinPlacanjaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artikli", t => t.ArtikalId)
                .ForeignKey("dbo.Korisnici", t => t.KupacId)
                .ForeignKey("dbo.NaciniPlacanja", t => t.NacinPlacanjaId)
                .ForeignKey("dbo.Korisnici", t => t.ProdavacId)
                .Index(t => t.ProdavacId)
                .Index(t => t.KupacId)
                .Index(t => t.ArtikalId)
                .Index(t => t.NacinPlacanjaId);
            
            CreateTable(
                "dbo.Korisnici",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                        Ulica = c.String(),
                        Broj = c.String(),
                        PostanskiBroj = c.String(),
                        Telefon = c.String(),
                        GradId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gradovi", t => t.GradId)
                .Index(t => t.GradId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisnici", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Gradovi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Korisnici", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.KorisniciUloge",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Korisnici", t => t.UserId)
                .ForeignKey("dbo.Uloge", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.NaciniPlacanja",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Opis = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Inkrementi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cijena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosInkrementa = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KomentariKorisnika",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        TextKomentara = c.String(),
                        KomentiraniKorisnikId = c.Int(nullable: false),
                        KomentatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisnici", t => t.KomentatorId)
                .ForeignKey("dbo.Korisnici", t => t.KomentiraniKorisnikId)
                .Index(t => t.KomentiraniKorisnikId)
                .Index(t => t.KomentatorId);
            
            CreateTable(
                "dbo.OcjeneArtikala",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        Ocjena = c.Int(nullable: false),
                        ArtikalId = c.Int(nullable: false),
                        OcijenioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artikli", t => t.ArtikalId)
                .ForeignKey("dbo.Korisnici", t => t.OcijenioId)
                .Index(t => t.ArtikalId)
                .Index(t => t.OcijenioId);
            
            CreateTable(
                "dbo.OcjeneKorisnika",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        OcijenjeniKorisnikId = c.Int(nullable: false),
                        OcjenjivacId = c.Int(nullable: false),
                        Ocjena = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisnici", t => t.OcijenjeniKorisnikId)
                .ForeignKey("dbo.Korisnici", t => t.OcjenjivacId)
                .Index(t => t.OcijenjeniKorisnikId)
                .Index(t => t.OcjenjivacId);
            
            CreateTable(
                "dbo.Ponude",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Iznos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vrijeme = c.DateTime(nullable: false),
                        MaksimalanIznos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KorisnikId = c.Int(nullable: false),
                        AukcijaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aukcije", t => t.AukcijaId)
                .ForeignKey("dbo.Korisnici", t => t.KorisnikId)
                .Index(t => t.KorisnikId)
                .Index(t => t.AukcijaId);
            
            CreateTable(
                "dbo.Poruke",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        TextPoruke = c.String(),
                        Procitana = c.Boolean(nullable: false),
                        PosiljalacId = c.Int(nullable: false),
                        PrimalacId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisnici", t => t.PosiljalacId)
                .ForeignKey("dbo.Korisnici", t => t.PrimalacId)
                .Index(t => t.PosiljalacId)
                .Index(t => t.PrimalacId);
            
            CreateTable(
                "dbo.Uloge",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KorisniciUloge", "RoleId", "dbo.Uloge");
            DropForeignKey("dbo.Poruke", "PrimalacId", "dbo.Korisnici");
            DropForeignKey("dbo.Poruke", "PosiljalacId", "dbo.Korisnici");
            DropForeignKey("dbo.Ponude", "KorisnikId", "dbo.Korisnici");
            DropForeignKey("dbo.Ponude", "AukcijaId", "dbo.Aukcije");
            DropForeignKey("dbo.OcjeneKorisnika", "OcjenjivacId", "dbo.Korisnici");
            DropForeignKey("dbo.OcjeneKorisnika", "OcijenjeniKorisnikId", "dbo.Korisnici");
            DropForeignKey("dbo.OcjeneArtikala", "OcijenioId", "dbo.Korisnici");
            DropForeignKey("dbo.OcjeneArtikala", "ArtikalId", "dbo.Artikli");
            DropForeignKey("dbo.KomentariKorisnika", "KomentiraniKorisnikId", "dbo.Korisnici");
            DropForeignKey("dbo.KomentariKorisnika", "KomentatorId", "dbo.Korisnici");
            DropForeignKey("dbo.Aukcije", "ProdavacId", "dbo.Korisnici");
            DropForeignKey("dbo.Aukcije", "NacinPlacanjaId", "dbo.NaciniPlacanja");
            DropForeignKey("dbo.Aukcije", "KupacId", "dbo.Korisnici");
            DropForeignKey("dbo.KorisniciUloge", "UserId", "dbo.Korisnici");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Korisnici");
            DropForeignKey("dbo.Korisnici", "GradId", "dbo.Gradovi");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Korisnici");
            DropForeignKey("dbo.Aukcije", "ArtikalId", "dbo.Artikli");
            DropForeignKey("dbo.Slike", "ArtikalId", "dbo.Artikli");
            DropForeignKey("dbo.Artikli", "KategorijaId", "dbo.Kategorije");
            DropIndex("dbo.Uloge", "RoleNameIndex");
            DropIndex("dbo.Poruke", new[] { "PrimalacId" });
            DropIndex("dbo.Poruke", new[] { "PosiljalacId" });
            DropIndex("dbo.Ponude", new[] { "AukcijaId" });
            DropIndex("dbo.Ponude", new[] { "KorisnikId" });
            DropIndex("dbo.OcjeneKorisnika", new[] { "OcjenjivacId" });
            DropIndex("dbo.OcjeneKorisnika", new[] { "OcijenjeniKorisnikId" });
            DropIndex("dbo.OcjeneArtikala", new[] { "OcijenioId" });
            DropIndex("dbo.OcjeneArtikala", new[] { "ArtikalId" });
            DropIndex("dbo.KomentariKorisnika", new[] { "KomentatorId" });
            DropIndex("dbo.KomentariKorisnika", new[] { "KomentiraniKorisnikId" });
            DropIndex("dbo.KorisniciUloge", new[] { "RoleId" });
            DropIndex("dbo.KorisniciUloge", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Korisnici", "UserNameIndex");
            DropIndex("dbo.Korisnici", new[] { "GradId" });
            DropIndex("dbo.Aukcije", new[] { "NacinPlacanjaId" });
            DropIndex("dbo.Aukcije", new[] { "ArtikalId" });
            DropIndex("dbo.Aukcije", new[] { "KupacId" });
            DropIndex("dbo.Aukcije", new[] { "ProdavacId" });
            DropIndex("dbo.Slike", new[] { "ArtikalId" });
            DropIndex("dbo.Artikli", new[] { "KategorijaId" });
            DropTable("dbo.Uloge");
            DropTable("dbo.Poruke");
            DropTable("dbo.Ponude");
            DropTable("dbo.OcjeneKorisnika");
            DropTable("dbo.OcjeneArtikala");
            DropTable("dbo.KomentariKorisnika");
            DropTable("dbo.Inkrementi");
            DropTable("dbo.NaciniPlacanja");
            DropTable("dbo.KorisniciUloge");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Gradovi");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Korisnici");
            DropTable("dbo.Aukcije");
            DropTable("dbo.Slike");
            DropTable("dbo.Kategorije");
            DropTable("dbo.Artikli");
        }
    }
}

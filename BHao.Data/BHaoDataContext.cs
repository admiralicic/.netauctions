using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHao.Business.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Runtime.Serialization;
using Microsoft.AspNet.Identity.EntityFramework;
namespace BHao.Data
{
    public class BHaoDataContext : IdentityDbContext<Korisnik,Uloga,int,Login,KorisnikUloga,KorisnikClaim>
    {
        public BHaoDataContext( )
            : base( "BHOnlineAukcije" )
        {

        }

        public static BHaoDataContext Create( )
        {
            return new BHaoDataContext( );
        }
        
        public DbSet<Aukcija> Aukcije { get; set; }
        public DbSet<Artikal> Artikli { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Inkrement> Inkrementi { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<KomentarKorisnika> KomentariKorisnika { get; set; }
        //public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<NacinPlacanja> NaciniPlacanja { get; set; }
        public DbSet<OcjenaArtikla> OcjeneArtikala { get; set; }
        public DbSet<OcjenaKorisnika> OcjeneKorisnika { get; set; }
        public DbSet<Ponuda> Ponude { get; set; }
        public DbSet<Poruka> Poruke { get; set; }
        public DbSet<Slika> Slike { get; set; }
        public DbSet<KomentarArtikla> KomentariArtikala { get; set; }

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>( );
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>( );
            modelBuilder.Ignore<ExtensionDataObject>( );

            modelBuilder.Entity<Aukcija>().HasMany<Slika>(x => x.Slike)
                                            .WithRequired(x => x.Aukcija)
                                            .HasForeignKey(x => x.AukcijaId);
            modelBuilder.Entity<Aukcija>().HasMany<Ponuda>(x => x.Ponude)
                                            .WithRequired(x => x.Aukcija)
                                            .HasForeignKey(x => x.AukcijaId);

            modelBuilder.Entity<Aukcija>( ).ToTable( "Aukcije" );
            modelBuilder.Entity<Artikal>( ).ToTable( "Artikli" );
            modelBuilder.Entity<Artikal>().Ignore(x => x.ProsjecnaOcjena);
            modelBuilder.Entity<Grad>( ).ToTable( "Gradovi" );
            modelBuilder.Entity<Inkrement>( ).ToTable( "Inkrementi" );
            modelBuilder.Entity<Kategorija>( ).ToTable( "Kategorije" );
            modelBuilder.Entity<KomentarKorisnika>( ).ToTable( "KomentariKorisnika" );
            //modelBuilder.Entity<Korisnik>().ToTable("Korisnici");
            modelBuilder.Entity<NacinPlacanja>( ).ToTable( "NaciniPlacanja" );
            modelBuilder.Entity<OcjenaArtikla>( ).ToTable( "OcjeneArtikala" );
            modelBuilder.Entity<OcjenaKorisnika>( ).ToTable( "OcjeneKorisnika" );
            modelBuilder.Entity<Ponuda>( ).ToTable( "Ponude" );
            modelBuilder.Entity<Poruka>( ).ToTable( "Poruke" );
            modelBuilder.Entity<Slika>( ).ToTable( "Slike" );
            modelBuilder.Entity<Uloga>( ).ToTable( "Uloge" );
            modelBuilder.Entity<KorisnikUloga>( ).ToTable( "KorisniciUloge" );
            modelBuilder.Entity<KomentarArtikla>().ToTable("KomentariArtikala");

            
            
        }
    }
}

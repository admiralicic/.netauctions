using BHao.Business.Entities;
using BHao.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Data.Contracts
{
    public interface IAukcijaRepository : IDataRepository<Aukcija>
    {
        IEnumerable<AukcijaDetailDTO> GetAll(bool? zavrsena);
        IEnumerable<Aukcija> GetAllNezavrsene();
        IEnumerable<AukcijaDetailDTO> GetAllAktivne(int kategorijaId, string stringZaPretragu);
        IEnumerable<Ponuda> GetPonude( int aukcijaId );
        Ponuda GetNajvecaPonuda( int aukcijaId );
        IEnumerable<AukcijaDetailDTO> GetAllZaKorisnika( int korisnikId, string kriterij );
        IEnumerable<Aukcija> GetAktivneZaKorisnika( int korisnikId );
        IEnumerable<Aukcija> GetZavrseneZaKorisnika( int korisnikId );
        AukcijaDTO GetAukcijaDetail(int aukcijaId, int korisnikId);
        IEnumerable<AukcijaDTO> GetAukcijeAktivneByArtikal(int artikalId);
        IEnumerable<AukcijaDetailDTO> GetAllZavrsene();
    }
}

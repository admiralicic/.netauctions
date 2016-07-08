using BHao.Business.Entities;
using BHao.Data.Common;
using BHao.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Data.Repositories
{
    public class InkrementRepository : DataRepositoryBase<Inkrement>, IInkrementRepository
    {
        public InkrementRepository( BHaoDataContext context )
            : base( context )
        {

        }

        public Inkrement GetInkrementByIznosPonude(decimal iznosNajvecePonude)
        {
            return _context.Inkrementi.Where(x => x.Cijena > iznosNajvecePonude).OrderBy(x => x.Cijena).Take(1).SingleOrDefault();
        }
    }
}

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
    public class KategorijaRepository : DataRepositoryBase<Kategorija>, IKategorijaRepository
    {
        public KategorijaRepository( BHaoDataContext context )
            : base( context )
        {

        }
    }
}

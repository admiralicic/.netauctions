using BHao.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Data.Contracts
{
    public interface IDataRepository
    {

    }

    public interface IDataRepository<T> where T : EntityBase
    {
        IEnumerable<T> GetAll( );
        T Get( int id );
        T Insert( T obj );
        T Update( T obj );
        int Delete( int id );
        int Delete( T obj );  
    }
}

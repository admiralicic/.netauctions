using BHao.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHao.Common.Core;
using System.Data.Entity;

namespace BHao.Data.Common
{
    public class DataRepositoryBase<T> : IDataRepository<T> where T : EntityBase
    {
        protected BHaoDataContext _context;

        public DataRepositoryBase( BHaoDataContext context )
        {
            _context = context;
        }


        public virtual IEnumerable<T> GetAll( )
        {
            return _context.Set<T>().ToList();
        }

        public virtual T Get( int id )
        {
            return _context.Set<T>( ).Find( id );
        }

        public virtual T Insert( T obj )
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges( );

            return obj;
        }

        public virtual T Update( T obj )
        {
            _context.Set<T>().Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges( );

            return obj;
        }

        public virtual int Delete( int id )
        {
            T obj = Get( id );

            return Delete( obj );

        }

        public virtual int Delete( T obj )
        {
            _context.Set<T>( ).Remove( obj );
            return _context.SaveChanges( );
        }
    }
}

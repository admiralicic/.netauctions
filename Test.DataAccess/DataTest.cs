using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BHao.Data;
using BHao.Data.Contracts;
using BHao.Data.Repositories;
using BHao.Business.Entities;

namespace Test.DataAccess
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void testing_repository_access( )
        {
            
            using ( BHaoDataContext context = new BHaoDataContext( ) )
            {
                IAukcijaRepository repo = new AukcijaRepository( context );

                Aukcija aukcija = repo.Get( 4 );

                Assert.IsTrue( aukcija != null );
            }
        }
    }
}

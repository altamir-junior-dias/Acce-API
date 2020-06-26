using System.Data;
using Bootstrapping.Repositories;
using Bootstrapping.Wrappers;
using FakeItEasy;

namespace Repositories
{
    public class UnitOfWork_TestBase 
    {
        protected IUnitOfWork unitOfWork;
        protected IDbConnection connection;
        protected IDbTransaction transaction;
        protected IGarbageCollectorWrapper garbageCollectorWrapper;

        public UnitOfWork_TestBase()  {
            connection = A.Fake<IDbConnection>();
            transaction = A.Fake<IDbTransaction>();
            garbageCollectorWrapper = A.Fake<IGarbageCollectorWrapper>();

            A.CallTo(() => connection.Open()).DoesNothing();
            A.CallTo(() => connection.BeginTransaction()).Returns(transaction);

            unitOfWork = new UnitOfWork(connection, garbageCollectorWrapper);
        }
    }
}
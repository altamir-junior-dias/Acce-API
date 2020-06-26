using System.Data;
using Bootstrapping.Repositories;
using Bootstrapping.Wrappers;
using FakeItEasy;

namespace Repositories
    {
    public class RepositoryBase_TestBase
    {
        protected SampleRepository sampleRepository;
        protected IUnitOfWork unitOfWork;
        protected IConnectionWrapper connection;
        protected IDbTransaction transaction;

        public RepositoryBase_TestBase() 
        {
            unitOfWork = A.Fake<IUnitOfWork>();
            transaction = A.Fake<IDbTransaction>();
            connection = A.Fake<IConnectionWrapper>();

            A.CallTo(() => unitOfWork.CurrentTransaction).Returns(transaction);
            A.CallTo(() => unitOfWork.Connection).Returns(connection);

            sampleRepository = new SampleRepository();
            sampleRepository.SetUnitOfWork(unitOfWork);
        }
    }
}
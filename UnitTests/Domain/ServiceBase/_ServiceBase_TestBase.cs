using AutoMapper;
using Bootstrapping.Domain;
using Bootstrapping.Repositories;
using FakeItEasy;

namespace Domain
{
    public class ServiceBase_TestBase
    {
        protected SampleService service;
        protected SampleRepositoryInterface repository;
        protected IUnitOfWork unitOfWork;
        protected IMapper mapper;

        public ServiceBase_TestBase() 
        {
            repository = A.Fake<SampleRepositoryInterface>();
            unitOfWork = A.Fake<IUnitOfWork>();
            mapper = A.Fake<IMapper>();

            A.CallTo(() => repository.SetUnitOfWork(A<IUnitOfWork>.Ignored)).DoesNothing();
            
            service = new SampleService(repository, unitOfWork, mapper);
        }
    }

    public class SampleService : ServiceBase<SampleEntity, SampleRecord, SampleRepositoryInterface> {
        public SampleService(SampleRepositoryInterface repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper) { }
    }
}
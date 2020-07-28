using AutoMapper;
using Acce.Domain;
using Acce.Repositories;
using Data.Entities;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    public class CustomersService : ServiceBase<Customer, CustomerRecord, ICustomersRepository>, ICustomersService
    {
        public CustomersService(ICustomersRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper) 
        {

        }
    }
}
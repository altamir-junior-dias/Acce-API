using Bootstrapping.Repositories;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories
{
    public class CustomersRepository : RepositoryBase<CustomerRecord>, ICustomersRepository
    {
        
    }
}
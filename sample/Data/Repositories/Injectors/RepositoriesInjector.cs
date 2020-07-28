using Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories.Injectors
{
    public static class RepositoriesInjector
    {
        public static void Config(IServiceCollection services)
		{
			services.AddScoped<ICustomersRepository, CustomersRepository>(); 
        }
    }
}
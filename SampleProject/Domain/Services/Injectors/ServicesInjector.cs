using Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Services.Injectors
{
    public static class ServicesInjector
    {
        public static void Config(IServiceCollection services)
		{
			services.AddScoped<ICustomersService, CustomersService>(); 
        }
    }
}
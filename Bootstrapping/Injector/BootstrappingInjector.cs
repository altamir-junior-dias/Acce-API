using Microsoft.Extensions.DependencyInjection;
using Bootstrapping.Wrappers;
using Bootstrapping.Repositories;

namespace Bootstrapping.Injector
{
    public static class BootstrappingInjector
    {
        public static void Config(IServiceCollection services)
        {
            services.AddScoped<IGarbageCollectorWrapper, GarbageCollectorWrapper>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

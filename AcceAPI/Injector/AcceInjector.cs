using Microsoft.Extensions.DependencyInjection;
using Acce.Wrappers;
using Acce.Repositories;

namespace Acce.Injector
{
    public static class AcceInjector
    {
        public static void Config(IServiceCollection services)
        {
            services.AddScoped<IGarbageCollectorWrapper, GarbageCollectorWrapper>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

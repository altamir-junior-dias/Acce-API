using System.Data;
using System.Data.SqlClient;
using API.Entities.Mappers;
using AutoMapper;
using Acce.Injector;
using Data.Repositories.Injectors;
using Domain.Entities.Mappers;
using Domain.Services.Injectors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ConfigureInjection(services);
        }

        private void ConfigureInjection(IServiceCollection services)
        {
            services.AddSingleton(ConfigureMapper());

            services.AddScoped<IDbConnection>(_ => GetConnection());
            BootstrappingInjector.Config(services);
            ServicesInjector.Config(services);
            RepositoriesInjector.Config(services);
        }

        private IMapper ConfigureMapper()
        {
            var mapperConfiguration = new MapperConfiguration(config => {
                ApiMapper.Config(config);
                DomainMapper.Config(config);
            });

            return  mapperConfiguration.CreateMapper();
        }

        private SqlConnection GetConnection() {
            var username = Configuration.GetValue<string>("userdb");
			var password = Configuration.GetValue<string>("password");
			var connectionString = Configuration.GetConnectionString("DBConnection");

            var connection = new SqlConnection();
            connection.ConnectionString = string.Format(connectionString, username, password);

            return connection;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
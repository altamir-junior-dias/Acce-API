using AutoMapper;
using Domain.Entities;

namespace API.Entities.Mappers
{
    internal static class CustomersMapper
    {
        public static void Config(IMapperConfigurationExpression config)
        {
            config.CreateMap<CustomerDTO, Customer>();
        }
    }
}
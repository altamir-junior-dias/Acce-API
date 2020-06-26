using AutoMapper;
using Data.Entities;
using Domain.Entities;

namespace Domain.Entities.Mappers
{
    internal static class CustomersMapper
    {
        public static void Config(IMapperConfigurationExpression config)
        {
            config.CreateMap<CustomerRecord, Customer>()
                .ForMember(domain => domain.Id, conf => conf.MapFrom(record => record.CustomerId));

            config.CreateMap<Customer, CustomerRecord>()
                .ForMember(record => record.CustomerId, conf => conf.MapFrom(domain => domain.Id));
        }
    }
}
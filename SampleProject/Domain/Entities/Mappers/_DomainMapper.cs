using AutoMapper;

namespace Domain.Entities.Mappers
{
    public static class DomainMapper
    {
        public static void Config(IMapperConfigurationExpression config)
        {
            CustomersMapper.Config(config);
        }
    }
}
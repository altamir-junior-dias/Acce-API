using AutoMapper;

namespace API.Entities.Mappers
{
    public static class ApiMapper
    {
        public static void Config(IMapperConfigurationExpression config)
        {
            CustomersMapper.Config(config);
        }
    }
}
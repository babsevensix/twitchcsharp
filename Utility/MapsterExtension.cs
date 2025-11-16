using Mapster;
using MapsterMapper;

public static class MapsterExtension
{
    public static IServiceCollection AddMapsterConfiguration(this IServiceCollection service)
    {
        var config = new TypeAdapterConfig();


        config.Scan(typeof(IndirizzoCittaMappers).Assembly);

        service.AddSingleton(config);

        service.AddScoped<IMapper, ServiceMapper>();

        return service;
    }
}
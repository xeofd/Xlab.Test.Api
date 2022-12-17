using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Xlab.Test.Api.IntegrationTests;

public static class ServiceCollectionExtensions
{
    private static IServiceCollection RemoveService<T>(this IServiceCollection services)
    {
        var serviceDescriptor = services.FirstOrDefault(x => x.ServiceType == typeof(T));
        if (serviceDescriptor is not null) services.Remove(serviceDescriptor);
        return services;
    }
    
    public static IServiceCollection AddMongoTestDatabase(this IServiceCollection services)
    {
        var dbName = $"xlab_testing_{Guid.NewGuid()}";
        return services.RemoveService<IMongoDatabase>().AddTransient<IMongoDatabase>(s =>
            s.GetRequiredService<IMongoClient>().GetDatabase(dbName));
    }
}
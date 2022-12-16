using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Mem.Data;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddData(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection
            .Configure<MongoDbOptions>(configuration.GetSection("MongoDb"))
            .AddTransient<MongoDbUrlBuilder>()
            .AddSingleton<IMongoClient>(sp => new MongoClient(sp.GetRequiredService<MongoDbUrlBuilder>().Build()))
            .AddTransient<IMongoDatabase>(sp =>
                sp.GetRequiredService<IMongoClient>()
                    .GetDatabase(configuration.GetValue<string>("MongoDb:DatabaseName", "test")));
        
        return serviceCollection;
    }
}
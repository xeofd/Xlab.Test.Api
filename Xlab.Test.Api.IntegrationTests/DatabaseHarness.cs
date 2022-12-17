using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Xlab.Test.Api.IntegrationTests.Businesses;
using Xlab.Test.Data;
using Xlab.Test.Domain.Businesses;

namespace Xlab.Test.Api.IntegrationTests;

public class DatabaseHarness
{
    private readonly Harness _rootHarness;
    
    static DatabaseHarness() => MongoDbConfigurator.Configure();
    public DatabaseHarness(Harness rootHarness) => _rootHarness = rootHarness;

    public async Task AddBusiness(Business business)
    {
        using var scope = _rootHarness.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IMongoDatabase>().GetBusinessesCollection();
        
        await db.InsertOneAsync(business);
    }

    public async Task AddBusinesses(int count)
    {
        for (var i = 0; i < count; i++)
        {
            await AddBusiness(new BusinessBuilder().WithName($"name-{Guid.NewGuid()}").Build());
        }
    }
}
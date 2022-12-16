using Xlab.Test.Data;

namespace Xlab.Test.Api.IntegrationTests;

public class DatabaseHarness
{
    private readonly Harness _rootHarness;
    
    static DatabaseHarness() => MongoDbConfigurator.Configure();
    public DatabaseHarness(Harness rootHarness) => _rootHarness = rootHarness;
}
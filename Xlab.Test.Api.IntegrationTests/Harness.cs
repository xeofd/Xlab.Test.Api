using Microsoft.AspNetCore.Mvc.Testing;

namespace Xlab.Test.Api.IntegrationTests;

public class Harness
{
    private readonly WebApplicationFactory<Program> _factory;
    private HttpClient Client => _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
    
    public IServiceProvider Services => _factory.Services;
    public DatabaseHarness Database { get; }
    
    public Harness(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        Database = new DatabaseHarness(this);
    }
}
using System.Net;
using System.Net.Http.Headers;
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

    public async Task<(HttpStatusCode statusCode, string response, HttpHeaders headers)> GetAllBusinesses()
    {
        var httpMessage = new HttpRequestMessage(HttpMethod.Get, "/businesses");
        var httpResponse = await Client.SendAsync(httpMessage);

        return (httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync(), httpResponse.Headers);
    }
}
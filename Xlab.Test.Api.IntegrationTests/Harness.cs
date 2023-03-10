using System.Net;
using System.Net.Http.Headers;
using System.Text;
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

    public async Task<(HttpStatusCode statusCode, string response, HttpHeaders headers)> GetAllBusinesses(string? search = null)
    {
        var searchQuery = search is not null ? $"?tag={search}" : "";
        var httpMessage = new HttpRequestMessage(HttpMethod.Get, $"/businesses{searchQuery}");
        var httpResponse = await Client.SendAsync(httpMessage);

        return (httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync(), httpResponse.Headers);
    }

    public async Task<(HttpStatusCode statusCode, string response, HttpHeaders headers)> GetABusiness(Guid businessId)
    {
        var httpMessage = new HttpRequestMessage(HttpMethod.Get, $"/businesses/{businessId}");
        var httpResponse = await Client.SendAsync(httpMessage);

        return (httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync(), httpResponse.Headers);
    }

    public async Task<(HttpStatusCode statusCode, string body)> CreateABusiness(string businessResource)
    {
        var httpMessage = new HttpRequestMessage(HttpMethod.Post, "/businesses");
        httpMessage.Content = new StringContent(businessResource, Encoding.UTF8, "application/json");
        
        var httpResponse = await Client.SendAsync(httpMessage);

        return (httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
    }
}
using System.Net;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Xlab.Test.Api.IntegrationTests.Businesses;

public class GettingAllBusinessesTests : IClassFixture<XLabWebApplicationFactory>
{
    private readonly Harness _harness;

    public GettingAllBusinessesTests(XLabWebApplicationFactory factory) => _harness = new Harness(factory);

    [Fact]
    public async Task GettingAllBusinesses_WhenNoBusinessesToReturn_ShouldReturn_200OK_AndEmptyArray()
    {
        var (statusCode, body) = await _harness.GetAllBusinesses();

        using var _ = new AssertionScope();
        statusCode.Should().Be(HttpStatusCode.OK);
        
        var response = JsonDocument.Parse(body).RootElement;
        response.GetArrayLength().Should().Be(0);
    }
}
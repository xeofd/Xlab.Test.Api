using System.Net;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Xlab.Test.Data;

namespace Xlab.Test.Api.IntegrationTests.Businesses;

public class GettingABusinessTests : IClassFixture<XLabWebApplicationFactory>, IAsyncLifetime
{
    private readonly Harness _harness;

    public GettingABusinessTests(XLabWebApplicationFactory factory) => _harness = new Harness(factory);
    
    public Task InitializeAsync() => Task.CompletedTask;

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _harness.Services.GetRequiredService<IMongoDatabase>().DropCollectionAsync(MongoDatabaseExtensions.BusinessesCollectionName);
    }

    [Fact]
    public async Task GettingABusiness_WithAValidId_ShouldReturn200Ok_AndBusinessInformation()
    {
        var business = new BusinessBuilder().WithName("Ben's Pub").Build();
        await _harness.Database.AddBusiness(business);
        
        var (statusCode, body, _) = await _harness.GetABusiness(business.id);

        using var _ = new AssertionScope();
        statusCode.Should().Be(HttpStatusCode.OK);

        var returnedItem = JsonDocument.Parse(body).RootElement;
        returnedItem.GetProperty("id").GetGuid().Should().Be(business.id);
        returnedItem.GetProperty("name").GetString().Should().Be(business.name);
        returnedItem.GetProperty("category").GetString().Should().Be(business.category);
        returnedItem.GetProperty("url").GetString().Should().Be(business.url);
        returnedItem.GetProperty("excerpt").GetString().Should().Be(business.excerpt);
        returnedItem.GetProperty("thumbnail").GetString().Should().Be(business.thumbnail);
        returnedItem.GetProperty("lat").GetDouble().Should().Be(business.lat);
        returnedItem.GetProperty("lng").GetDouble().Should().Be(business.lng);
        returnedItem.GetProperty("address").GetString().Should().Be(business.address);
        returnedItem.GetProperty("phone").GetString().Should().Be(business.phone);
        returnedItem.GetProperty("twitter").GetString().Should().Be(business.twitter);
        returnedItem.GetProperty("date").GetDateTime().Should().BeCloseTo(business.date, TimeSpan.FromSeconds(10));
        returnedItem.GetProperty("tags").GetArrayLength().Should().Be(2);
        returnedItem.GetProperty("stars").GetProperty("beer").GetDouble().Should().Be(business.stars_beer);
        returnedItem.GetProperty("stars").GetProperty("atmosphere").GetDouble().Should().Be(business.stars_atmosphere);
        returnedItem.GetProperty("stars").GetProperty("amenities").GetDouble().Should().Be(business.stars_amenities);
        returnedItem.GetProperty("stars").GetProperty("value").GetDouble().Should().Be(business.stars_value);
    }

    [Fact]
    public async Task GettingABusiness_WithInvalidId_ShouldReturn404NotFound()
    {
        var business = new BusinessBuilder().WithName("Ben's Pub").Build();
        await _harness.Database.AddBusiness(business);
        
        var (statusCode, _, _) = await _harness.GetABusiness(Guid.NewGuid());

        using var _ = new AssertionScope();
        statusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
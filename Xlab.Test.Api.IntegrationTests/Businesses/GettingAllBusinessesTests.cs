using System.Net;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Xlab.Test.Data;

namespace Xlab.Test.Api.IntegrationTests.Businesses;

public sealed class GettingAllBusinessesTests : IClassFixture<XLabWebApplicationFactory>, IAsyncLifetime
{
    private readonly Harness _harness;

    public GettingAllBusinessesTests(XLabWebApplicationFactory factory) => _harness = new Harness(factory);
    
    public Task InitializeAsync() => Task.CompletedTask;

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _harness.Services.GetRequiredService<IMongoDatabase>().DropCollectionAsync(MongoDatabaseExtensions.BusinessesCollectionName);
    }

    [Fact]
    public async Task GettingAllBusinesses_WhenNoBusinessesToReturn_ShouldReturn_200OK_AndEmptyArray()
    {
        var (statusCode, body, _) = await _harness.GetAllBusinesses();

        using var _ = new AssertionScope();
        statusCode.Should().Be(HttpStatusCode.OK);
        
        var response = JsonDocument.Parse(body).RootElement;
        response.GetArrayLength().Should().Be(0);
    }

    [Fact]
    public async Task GettingAllBusinesses_WhenBusinessesExistInDatabase_ShouldReturnArrayOfBusinesses()
    {
        var business = new BusinessBuilder().WithName("Ben's Pub").Build();
        await _harness.Database.AddBusiness(business);
        
        var (statusCode, body, _) = await _harness.GetAllBusinesses();

        using var _ = new AssertionScope();
        statusCode.Should().Be(HttpStatusCode.OK);
        
        var response = JsonDocument.Parse(body).RootElement;
        response.GetArrayLength().Should().Be(1);

        var returnedItem = response.EnumerateArray().FirstOrDefault();
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
    public async Task GettingAllBusinesses_ShouldReturnPaginatedResponse_WithMaximumOf10ItemsPerPage()
    {
        await _harness.Database.AddBusinesses(15);
        
        var (statusCode, body, headers) = await _harness.GetAllBusinesses();

        using var _ = new AssertionScope();
        statusCode.Should().Be(HttpStatusCode.OK);
        
        var response = JsonDocument.Parse(body).RootElement;
        response.GetArrayLength().Should().Be(10);

        headers.NonValidated["Link"].ToString().Should().Be("/Businesses?pageId=1&pageSize=10; rel=first, /Businesses?pageId=2&pageSize=10; rel=next, /Businesses?pageId=2&pageSize=10; rel=last");
    }

    [Fact]
    public async Task GettingAllBusinesses_SearchingUsingTags_ShouldReturnOnlyResultsWithCorrectTags()
    {
        var businessWithTags = new BusinessBuilder().WithName("Ben's Pub").WithTags(new []{ "live music", "sofa" }).Build();
        var businessWitoutTags = new BusinessBuilder().WithName("Ben's Pub without tags").Build();
        
        await _harness.Database.AddBusiness(businessWithTags);
        await _harness.Database.AddBusiness(businessWitoutTags);
        
        var (statusCode, body, _) = await _harness.GetAllBusinesses(search: "sofa");

        using var _ = new AssertionScope();
        statusCode.Should().Be(HttpStatusCode.OK);
        
        var response = JsonDocument.Parse(body).RootElement;
        response.GetArrayLength().Should().Be(1);
        var returnedItem = response.EnumerateArray().FirstOrDefault();
        returnedItem.GetProperty("id").GetGuid().Should().Be(businessWithTags.id);
    }
}
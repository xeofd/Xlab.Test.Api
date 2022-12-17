using System.Net;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Xlab.Test.Api.Businesses;
using Xlab.Test.Data;

namespace Xlab.Test.Api.IntegrationTests.Businesses;

public class CreatingABusinessTests : IClassFixture<XLabWebApplicationFactory>, IAsyncLifetime
{
    private readonly Harness _harness;

    public CreatingABusinessTests(XLabWebApplicationFactory factory) => _harness = new Harness(factory);

    public Task InitializeAsync() => Task.CompletedTask;

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _harness.Services.GetRequiredService<IMongoDatabase>()
            .DropCollectionAsync(MongoDatabaseExtensions.BusinessesCollectionName);
    }

    [Fact]
    public async Task CreatingABusiness_WithAValidRequest_ShouldReturn201Created_WithCreatedId_AndExistInDatabase()
    {
        var business = new BusinessBuilder().Build();
        var businessResource = BusinessJsonBuilder.Build(business);

        var (statusCode, body) = await _harness.CreateABusiness(businessResource.ToString());

        using var _ = new AssertionScope();
        statusCode.Should().Be(HttpStatusCode.Created);

        var response = JsonDocument.Parse(body).RootElement;
        var id = response.GetProperty("id").GetGuid();

        id.Should().NotBeEmpty();

        var dbResult = await _harness.Database.GetBusinessById(id);
        dbResult.id.Should().Be(id);
    }

    [Fact]
    public async Task CreatingABusiness_WithInvalidRequest_ShouldReturn400BadRequest()
    {
        var (statusCode, body) = await _harness.CreateABusiness("{}");

        using var _ = new AssertionScope();
        statusCode.Should().Be(HttpStatusCode.BadRequest); 
    }

}
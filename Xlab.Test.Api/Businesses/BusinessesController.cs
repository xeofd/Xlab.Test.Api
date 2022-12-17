using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Xlab.Test.Data;
using Xlab.Test.Domain.Businesses;

namespace Xlab.Test.Api.Businesses;

[ApiController]
[Route("[controller]")]
public class BusinessesController : Controller
{
    private readonly IMongoCollection<Business> _collection;

    public BusinessesController(IMongoDatabase database) => _collection = database.GetBusinessesCollection();

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationQueryParameters pagination,
        [FromQuery] string? tag,
        CancellationToken cancellationToken)
    {
        var allResults = Builders<Business>.Filter.Empty;
        var searchByTags = Builders<Business>.Filter.Where(b => b.tags.Contains(tag));

        var businesses = (await _collection.FindAsync(tag is not null ? searchByTags : allResults, cancellationToken: cancellationToken))
            .ToList(cancellationToken)
            .AsQueryable();

        return this.PagedOk(businesses, BusinessResourceRepresentation.From, pagination.PageId ?? 1,
            pagination.PageSize ?? 10);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var businessQuery = Builders<Business>.Filter.Eq(b => b.id, id);
        var business = (await _collection.FindAsync(businessQuery, cancellationToken: cancellationToken)).FirstOrDefault(cancellationToken);

        if (business is null) return NotFound();

        return Ok(BusinessResourceRepresentation.From(business));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostBusinessResourceRepresentation resource,
        CancellationToken cancellationToken)
    {
        var createdBusiness = BusinessBuilder.From(resource);

        await _collection.InsertOneAsync(createdBusiness, cancellationToken: cancellationToken);

        return CreatedAtAction("GetById", new { id = createdBusiness.id },
            new PostBusinessResponseResourceRepresentation { Id = createdBusiness.id });
    }
}
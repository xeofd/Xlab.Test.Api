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
    public async Task<IActionResult> GetAll([FromQuery] PaginationQueryParameters pagination,
        CancellationToken cancellationToken)
    {
        var businesses = (await _collection.FindAsync(b => true, cancellationToken: cancellationToken))
            .ToList(cancellationToken)
            .AsQueryable();

        return this.PagedOk(businesses, BusinessResourceRepresentation.From, pagination.PageId ?? 1,
            pagination.PageSize ?? 10);
    }
}
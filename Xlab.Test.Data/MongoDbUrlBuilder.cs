using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mem.Data;

public class MongoDbUrlBuilder
{
    private readonly IOptions<MongoDbOptions> _options;

    public MongoDbUrlBuilder(IOptions<MongoDbOptions> options) => _options = options;

    public MongoUrl Build()
    {
        var buildFormsDbMongoUri = new MongoUrlBuilder(_options.Value.ConnectionString);

        if (_options.Value.Password is { Length: > 0 } password)
        {
            buildFormsDbMongoUri.Password = password;
        }

        return buildFormsDbMongoUri.ToMongoUrl();
    }
}
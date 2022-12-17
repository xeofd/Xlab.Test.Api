using Newtonsoft.Json.Linq;
using Xlab.Test.Domain.Businesses;

namespace Xlab.Test.Api.IntegrationTests.Businesses;

public static class BusinessJsonBuilder
{
    public static JObject Build(Business business)
    {
        var starsJObject = new JObject
        {
            { "name", business.name },
            { "category", business.category },
            { "url", business.url },
            { "excerpt", business.excerpt }
        };
        
        return new JObject
        {
            { "name", business.name },
            { "category", business.category },
            { "url", business.url },
            { "excerpt", business.excerpt },
            { "thumbnail", business.thumbnail },
            { "lat", business.lat },
            { "lng", business.lng },
            { "address", business.address },
            { "phone", business.phone },
            { "twitter", business.twitter },
            { "stars", starsJObject },
            { "date", business.date },
            { "tags", new JArray(business.tags) }
        };
    }
}
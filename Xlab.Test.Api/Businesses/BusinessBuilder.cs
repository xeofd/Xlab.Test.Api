using Xlab.Test.Domain.Businesses;

namespace Xlab.Test.Api.Businesses;

public static class BusinessBuilder
{
    public static Business From(PostBusinessResourceRepresentation resource)
    {
        return new Business(
            Guid.NewGuid(),
            resource.Name,
            resource.Category,
            resource.Url,
            resource.Excerpt,
            resource.Thumbnail,
            resource.Lat,
            resource.Lng,
            resource.Address,
            resource.Phone,
            resource.Twitter,
            resource.Stars.Beer,
            resource.Stars.Atmosphere,
            resource.Stars.Amenities,
            resource.Stars.Value,
            resource.Date,
            resource.Tags
        );
    }
}
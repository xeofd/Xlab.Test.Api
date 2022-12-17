using Xlab.Test.Domain.Businesses;

namespace Xlab.Test.Api.Businesses;

public class BusinessResourceRepresentation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Url { get; set; }
    public string Excerpt { get; set; }
    public string Thumbnail { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Twitter { get; set; }
    public BusinessStarsResourceRepresentation Stars { get; set; }
    public DateTime Date { get; set; }
    public IEnumerable<string> Tags { get; set; }

    public static BusinessResourceRepresentation From(Business business)
    {
        return new BusinessResourceRepresentation
        {
            Id = business.id,
            Name = business.name,
            Category = business.category,
            Url = business.url,
            Excerpt = business.excerpt,
            Thumbnail = business.thumbnail,
            Lat = business.lat,
            Lng = business.lng,
            Address = business.address,
            Phone = business.phone,
            Twitter = business.twitter,
            Stars = BusinessStarsResourceRepresentation.From(business),
            Date = business.date,
            Tags = business.tags
        };
    }
}

public class BusinessStarsResourceRepresentation
{
    public int Beer { get; set; }
    public int Atmosphere { get; set; }
    public int Amenities { get; set; }
    public int Value { get; set; }

    public static BusinessStarsResourceRepresentation From(Business business) => new()
    {
        Beer = business.stars_beer, Amenities = business.stars_amenities, Atmosphere = business.stars_atmosphere,
        Value = business.stars_value
    };
}
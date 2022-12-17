using Xlab.Test.Domain.Businesses;

namespace Xlab.Test.Api.Businesses;

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
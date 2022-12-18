using Xlab.Test.Domain.Businesses;

namespace Xlab.Test.Api.Businesses;

// star values set to double as mongodb driver was freaking out about them when set to int like they should be and I'm not sure why

public class BusinessStarsResourceRepresentation
{
    public double Beer { get; set; }
    public double Atmosphere { get; set; }
    public double Amenities { get; set; }
    public double Value { get; set; }

    public static BusinessStarsResourceRepresentation From(Business business) => new()
    {
        Beer = business.stars_beer, Amenities = business.stars_amenities, Atmosphere = business.stars_atmosphere,
        Value = business.stars_value
    };
}
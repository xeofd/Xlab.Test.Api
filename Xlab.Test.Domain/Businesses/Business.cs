namespace Xlab.Test.Domain.Businesses;

// star values set to double as mongodb driver was freaking out about them when set to int like they should be and I'm not sure why

public record Business(
    Guid id,
    string name,
    string category,
    string url,
    string excerpt,
    string thumbnail,
    double lat,
    double lng,
    string address,
    string phone,
    string twitter,
    double stars_beer,
    double stars_atmosphere,
    double stars_amenities,
    double stars_value,
    DateTime date,
    IEnumerable<string> tags
);
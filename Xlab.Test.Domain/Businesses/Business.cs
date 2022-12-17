namespace Xlab.Test.Domain.Businesses;

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
    int stars_beer,
    int stars_atmosphere,
    int stars_amenities,
    int stars_value,
    DateTime date,
    IEnumerable<string> tags
);
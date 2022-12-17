using Xlab.Test.Domain.Businesses;

namespace Xlab.Test.Api.IntegrationTests.Businesses;

public class BusinessBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _name = "Name";
    private string _category = "Category";
    private string _url = "https://localhost/";
    private string _excerpt = "A quick description of business";
    private string _thumbnail = "https://localhost/thumbnail";
    private double _lat = 0.0;
    private double _lng = 0.0;
    private string _address = "1 Example Lane, Example Town EX1 1EX";
    private string _phone = "07000000000";
    private string _twitter = "TwitterHandle";
    private int _starsBeer = 0;
    private int _starsAtmosphere = 0;
    private int _starsAmenities = 0;
    private int _starsValue = 0;
    private DateTime _date = DateTime.Now;
    private IEnumerable<string> _tags = new List<string> { "Tag1", "Tag2" };

    public BusinessBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public Business Build()
    {
        return new Business(
            _id,
            _name,
            _category,
            _url,
            _excerpt,
            _thumbnail,
            _lat,
            _lng,
            _address,
            _phone,
            _twitter,
            _starsBeer,
            _starsAtmosphere,
            _starsAmenities,
            _starsValue,
            _date,
            _tags);
    }
}
namespace Xlab.Test.Api.Businesses;

public class PostBusinessResourceRepresentation
{
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
}
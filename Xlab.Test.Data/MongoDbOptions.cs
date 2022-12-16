namespace Mem.Data;

public class MongoDbOptions
{
    public string ConnectionString { get; set; } = null!;
    public string? Password { get; set; }
}
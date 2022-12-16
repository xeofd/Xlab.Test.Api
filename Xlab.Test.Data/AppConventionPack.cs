using MongoDB.Bson.Serialization.Conventions;

namespace Mem.Data;

public class AppConventionPack : IConventionPack
{
    public IEnumerable<IConvention> Conventions { get; }

    private AppConventionPack() => Conventions = new List<IConvention>
    {
        new IgnoreIfNullConvention(true),
        new IgnoreExtraElementsConvention(true)
    };
    
    public static IConventionPack Instance { get; } = new AppConventionPack();
}
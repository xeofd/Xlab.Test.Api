using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace Mem.Data;

public static class MongoDbConfigurator
{
    private static readonly object Lock = new();
    private static bool _ranOnce;
    
    public static void Configure()
    {
        lock (Lock)
        {
            if (_ranOnce) return;
            
            InnerConfigure();
            _ranOnce = true;
        }
    }

    private static void InnerConfigure()
    {
        ConventionRegistry.Register("AppConventions", AppConventionPack.Instance, t => t.Namespace?.StartsWith("Xlab.Test.Domain", StringComparison.InvariantCulture) == true);
        
#pragma warning disable 618
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
#pragma warning restore 618
        
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }
}
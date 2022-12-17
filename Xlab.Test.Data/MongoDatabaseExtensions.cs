using MongoDB.Driver;
using Xlab.Test.Domain.Businesses;

namespace Xlab.Test.Data;

public static class MongoDatabaseExtensions
{
    public const string BusinessesCollectionName = "businesses";
    public static IMongoCollection<Business> GetBusinessesCollection(this IMongoDatabase db) => db.GetCollection<Business>(BusinessesCollectionName);
}
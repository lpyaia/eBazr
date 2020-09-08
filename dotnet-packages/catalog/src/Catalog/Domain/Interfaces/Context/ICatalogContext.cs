using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Catalog.Domain.Interfaces.Context
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}

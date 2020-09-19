using Catalog.Domain.Interfaces.Settings;

namespace Catalog.CrossCutting
{
    public class CatalogDatabaseSettings : ICatalogDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
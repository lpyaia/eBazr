using System.Collections.Generic;
using System.Threading.Tasks;
using Web.App.Models;

namespace Web.App.ApiCollection.Interfaces
{
    public interface ICatalogApi
    {
        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);

        Task<IEnumerable<CatalogModel>> GetCatalog();

        Task<CatalogModel> GetCatalog(string id);

        Task<CatalogModel> CreateCatalog(CatalogModel model);
    }
}
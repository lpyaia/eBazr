using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Domain.QuerySide.Queries
{
    public class GetProductQuery : IRequest<Product>
    {
        public string Id { get; set; }
    }
}
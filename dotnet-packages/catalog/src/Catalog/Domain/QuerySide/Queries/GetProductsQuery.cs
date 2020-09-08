using Catalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Catalog.Domain.QuerySide.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}

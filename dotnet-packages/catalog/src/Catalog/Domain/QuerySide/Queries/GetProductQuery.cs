using Catalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Catalog.Domain.QuerySide.Queries
{
    public class GetProductByCategory : IRequest<IEnumerable<Product>>
    {
        public string Category { get; set; }
    }
}
using Catalog.Domain.Entities;
using Catalog.Domain.Interfaces.Repositories;
using Catalog.Domain.QuerySide.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Domain.QuerySide.QueryHandlers
{
    public class GetProductByCategoryHandler : IRequestHandler<GetProductByCategory, IEnumerable<Product>>
    {
        private readonly IProductRepository _repository;

        public GetProductByCategoryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductByCategory request, CancellationToken cancellationToken)
        {
            return await _repository.GetProductByCategory(request.Category);
        }
    }
}
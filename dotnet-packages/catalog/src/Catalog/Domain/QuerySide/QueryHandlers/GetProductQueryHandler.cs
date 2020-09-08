using Catalog.Domain.Entities;
using Catalog.Domain.Interfaces.Repositories;
using Catalog.Domain.QuerySide.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Domain.QuerySide.QueryHandlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly IProductRepository _repository;

        public GetProductQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetProduct(request.Id);
        }
    }
}

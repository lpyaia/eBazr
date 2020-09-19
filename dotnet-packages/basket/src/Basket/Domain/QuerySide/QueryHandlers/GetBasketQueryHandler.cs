using Basket.Domain.Entities;
using Basket.Domain.Interfaces.Repositories;
using Basket.Domain.QuerySide.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Domain.QuerySide.QueryHandlers
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketCart>
    {
        private readonly IBasketRepository _repository;

        public GetBasketQueryHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<BasketCart> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetBasket(request.UserName);
        }
    }
}
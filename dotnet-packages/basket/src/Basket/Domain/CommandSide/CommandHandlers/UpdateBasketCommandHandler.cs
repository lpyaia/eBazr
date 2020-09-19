using Basket.Domain.CommandSide.Commands;
using Basket.Domain.Entities;
using Basket.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Domain.CommandSide.CommandHandlers
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, BasketCart>
    {
        private readonly IBasketRepository _repository;

        public UpdateBasketCommandHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<BasketCart> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateBasket(request.BasketCart);
        }
    }
}
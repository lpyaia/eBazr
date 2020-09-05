using Basket.Domain.CommandSide.Commands;
using Basket.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Domain.CommandSide.CommandHandlers
{
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, bool>
    {
        private readonly IBasketRepository _repository;

        public DeleteBasketCommandHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteBasket(request.UserName);
        }
    }
}

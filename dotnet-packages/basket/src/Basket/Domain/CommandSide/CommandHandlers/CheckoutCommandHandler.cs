using AutoMapper;
using Basket.Core.Common;
using Basket.Domain.CommandSide.Commands;
using Basket.Domain.Events;
using Basket.Domain.Interfaces.Repositories;
using Common.Core.Bus;
using Common.Core.Logging;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Domain.CommandSide.CommandHandlers
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, bool>
    {
        private readonly IBasketRepository _repository;
        private readonly IBusPublisher _publisher;
        private readonly IMapper _mapper;

        public CheckoutCommandHandler(IBasketRepository repository, 
            IBusPublisher publisher, 
            IMapper mapper)
        {
            _repository = repository;
            _publisher = publisher;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var basket = await _repository.GetBasket(request.BasketCheckout.UserName);
            if (basket == null)
            {
                LogHelper.Error($"Basket not exist with this user : {request.BasketCheckout.UserName}");
                return false;
            }

            var basketRemoved = await _repository.DeleteBasket(request.BasketCheckout.UserName);
            
            if (!basketRemoved)
            {
                LogHelper.Error("Basket can not deleted");
                return false;
            }

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(request.BasketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;

            await _publisher.PublishAsync(ContextNames.Exchange.BasketCheckout, eventMessage);

            return true;
        }
    }
}

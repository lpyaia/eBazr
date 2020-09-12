using AutoMapper;
using Common.Core.Bus.Consumer;
using MediatR;
using Order.Domain.CommandSide.Commands;
using Order.Domain.Events;
using System.Threading.Tasks;

namespace Order.Consumer.Consumers
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BasketCheckoutConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Execute(BasketCheckoutEvent message)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(message);
            await _mediator.Send(command);
        }
    }
}

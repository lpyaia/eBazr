using MediatR;
using Order.Domain.CommandSide.Commands;
using Order.Domain.Interfaces.Repositories;
using Order.Domain.Mappings;
using Order.Domain.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;
using Entity = Order.Domain.Entities;

namespace Order.Domain.CommandSide.CommandHandlers
{
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public CheckoutOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OrderResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = OrderMapper.Mapper.Map<Entity.Order>(request);

            if (orderEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newOrder = await _orderRepository.AddAsync(orderEntity);

            var orderResponse = OrderMapper.Mapper.Map<OrderResponse>(newOrder);
            return orderResponse;
        }
    }
}

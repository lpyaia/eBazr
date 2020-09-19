using Basket.Domain.Entities;
using MediatR;

namespace Basket.Domain.CommandSide.Commands
{
    public class CheckoutCommand : IRequest<bool>
    {
        public BasketCheckout BasketCheckout { get; set; }
    }
}
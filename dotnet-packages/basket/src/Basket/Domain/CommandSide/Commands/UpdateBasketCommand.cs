using Basket.Domain.Entities;
using MediatR;

namespace Basket.Domain.CommandSide.Commands
{
    public class UpdateBasketCommand : IRequest<BasketCart>
    {
        public BasketCart BasketCart { get; set; }
    }
}
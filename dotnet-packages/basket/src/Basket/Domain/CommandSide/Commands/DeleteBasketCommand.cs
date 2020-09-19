using MediatR;

namespace Basket.Domain.CommandSide.Commands
{
    public class DeleteBasketCommand : IRequest<bool>
    {
        public string UserName { get; set; }
    }
}
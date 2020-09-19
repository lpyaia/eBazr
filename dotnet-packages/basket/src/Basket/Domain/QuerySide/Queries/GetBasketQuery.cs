using Basket.Domain.Entities;
using MediatR;

namespace Basket.Domain.QuerySide.Queries
{
    public class GetBasketQuery : IRequest<BasketCart>
    {
        public string UserName { get; set; }
    }
}
using AutoMapper;
using Basket.Domain.Entities;
using Basket.Domain.Events;

namespace Basket.Domain.Mappings
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}

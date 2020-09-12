using AutoMapper;
using Order.Domain.CommandSide.Commands;
using Order.Domain.Events;
using Order.Domain.Responses;

namespace Order.Domain.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Entities.Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Entities.Order, OrderResponse>().ReverseMap();
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
        }
    }
}

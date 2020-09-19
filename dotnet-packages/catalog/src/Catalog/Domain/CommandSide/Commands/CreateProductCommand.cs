using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Domain.CommandSide.Commands
{
    public class CreateProductCommand : IRequest<bool>
    {
        public Product Product { get; set; }
    }
}
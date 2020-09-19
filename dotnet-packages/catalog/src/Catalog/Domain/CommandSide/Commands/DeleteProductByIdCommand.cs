using MediatR;

namespace Catalog.Domain.CommandSide.Commands
{
    public class DeleteProductByIdCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
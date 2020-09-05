using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Domain.CommandSide.Commands
{
    public class DeleteBasketCommand : IRequest<bool>
    {
        public string UserName { get; set; }
    }
}

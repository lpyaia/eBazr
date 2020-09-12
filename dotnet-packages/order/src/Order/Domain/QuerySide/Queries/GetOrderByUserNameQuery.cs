﻿using MediatR;
using Order.Domain.Responses;
using System;
using System.Collections.Generic;

namespace Order.Domain.QuerySide.Queries
{
    public class GetOrderByUserNameQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public string UserName { get; set; }

        public GetOrderByUserNameQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Web.App.Models;

namespace Web.App.ApiCollection.Interfaces
{
    public interface IOrderApi
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
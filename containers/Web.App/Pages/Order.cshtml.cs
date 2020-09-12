using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.App.ApiCollection.Interfaces;
using Web.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.App
{
    public class OrderModel : PageModel
    {
        private readonly IOrderApi _orderApi;

        public OrderModel(IOrderApi orderApi)
        {
            _orderApi = orderApi ?? throw new ArgumentNullException(nameof(orderApi));
        }

        public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            Orders = await _orderApi.GetOrdersByUserName("swn");

            return Page();
        }       
    }
}
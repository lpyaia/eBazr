using Web.App.ApiCollection.Infrastructure;
using Web.App.ApiCollection.Interfaces;
using Web.App.Models;
using Web.App.Settings;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.App.ApiCollection
{
    public class OrderApi : BaseHttpClientWithFactory, IOrderApi
    {
        private readonly IApiSettings _settings;

        public OrderApi(IHttpClientFactory factory, IApiSettings settings)
            : base(factory)
        {
            _settings = settings;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                           .SetPath(_settings.OrderPath)
                           .AddQueryString("username", userName)
                           .HttpMethod(HttpMethod.Get)
                           .GetHttpMessage();

            return await SendRequest<IEnumerable<OrderResponseModel>>(message);
        }
    }
}

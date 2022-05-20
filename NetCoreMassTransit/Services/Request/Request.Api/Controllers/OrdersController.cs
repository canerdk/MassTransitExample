using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Request.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IRequestClient<CheckOrderStatus> _client;
        public OrdersController(IRequestClient<CheckOrderStatus> client)
        {
            _client = client;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _client.GetResponse<OrderStatusResult>(new { OrderId = id });

            return Ok(response.Message);
        }


    }

    
}

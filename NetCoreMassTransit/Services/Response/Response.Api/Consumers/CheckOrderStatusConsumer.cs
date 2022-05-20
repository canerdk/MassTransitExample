using EventBus.Messages.Events;
using MassTransit;
using Response.Api.Repositories.Abstract;

namespace Response.Api.Consumers
{
    public class CheckOrderStatusConsumer : IConsumer<CheckOrderStatus>
    {
        private readonly IOrderRepository _orderRepository;
        public CheckOrderStatusConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<CheckOrderStatus> context)
        {
            var order = await _orderRepository.GetById(context.Message.OrderId);
            if (order == null)
                throw new InvalidOperationException("Order not found");

            await context.RespondAsync<OrderStatusResult>(new
            {
                OrderId = order.Id,
                TimeStamp = DateTime.Now,
                StatusText = order.OrderNo + " success"
            });
        }
    }
}

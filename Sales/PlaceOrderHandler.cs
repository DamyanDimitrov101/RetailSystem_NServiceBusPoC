using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrderCommand>
    {
        public async Task Handle(PlaceOrderCommand message, IMessageHandlerContext context)
        {
            log.Info($"Received PlaceOrder, OrderId = {message.OrderId}");

            await context.Publish(new OrderPlaced { OrderId = message.OrderId });
        }

        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();
    }
}

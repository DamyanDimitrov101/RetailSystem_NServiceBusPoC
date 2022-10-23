using NServiceBus;

namespace Messages;
public class PlaceOrderCommand : ICommand
{
    public string OrderId { get; set; }
}

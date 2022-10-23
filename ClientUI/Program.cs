using Messages;
using NServiceBus;

namespace ClientUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = "ClientUI";

            var endpointConfiguration = new EndpointConfiguration("ClientUI");

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var routing = transport.Routing();

            routing.RouteToEndpoint(typeof(PlaceOrderCommand), "Sales");

            endpointConfiguration.SendOnly();

            var endpoint = await Endpoint.Start(endpointConfiguration);

            await RunLoop(endpoint);

            await endpoint.Stop();
        }

        static async Task RunLoop(IMessageSession endpoint)
        {
            while (true)
            {
                Console.WriteLine("P to place order, Q to exit");
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        await endpoint.Send(new PlaceOrderCommand { OrderId = Guid.NewGuid().ToString()[..8] });
                    break;

                    case ConsoleKey.Q:
                        return;
                }
            }
        }
    }
}
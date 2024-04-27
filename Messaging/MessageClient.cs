using EasyNetQ;

namespace Messaging
{
    public class MessageClient
    {
        private readonly IBus _bus;

        public MessageClient(IBus bus)
        {
            _bus = bus;
        }

        public async Task Send<T>(T message, string topic)
        {
            await Task.Run(() => { _bus.PubSub.PublishAsync(message, topic); });
        }

        public async Task Listen<T>(Action<T> handler, string topic)
        {
            await Task.Run(() => { _bus.PubSub.SubscribeAsync(topic, handler); });
        }


    }
}

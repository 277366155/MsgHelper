using MH.Common;
using System;
using System.Text;

namespace MH.RabbitMq
{
    public class Publisher : BaseRabbitMQFactory
    {
        public Publisher(RabbitMqOptions options) : base(options)
        {

        }

        public void PublisherStart<T>(T obj)
        {
            if (obj == null)
            {
                return;
            }
            var body = Encoding.UTF8.GetBytes(obj.ToJson());
            Channel.BasicPublish(_options.ExchangeName, _options.RoutingKey, false, null, body);
        }

        public new void Dispose()
        {
            base.Dispose();
        }
        ~Publisher()
        {
            Dispose();
        }
    }
}

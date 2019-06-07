using MH.Common;
using System;
using System.Text;

namespace MH.RabbitMq
{
    public class Publisher : BaseRabbitMQFactory
    {
        public Publisher(RabbitMqOptions options) : base(options)
        {
            //绑定Exchange与Queue的路由关系
            Channel.QueueBind(options.QueueName, options.ExchangeName, options.RoutingKey, null);
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

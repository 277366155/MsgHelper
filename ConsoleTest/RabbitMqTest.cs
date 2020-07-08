using MH.Core;
using MH.RabbitMq;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MH.ConsoleTest
{
    public class RabbitMqTest
    {
        static RabbitMqOptions _options = BaseCore.Configuration.GetSection("RabbitMqConfig").Get<RabbitMqOptions>();
		static RabbitMqOptions options2 = BaseCore.Configuration.GetSection("RabbitMqConfig2").Get<RabbitMqOptions>();
        public void Consumer(RabbitMqOptions options)
        {
            if (options != null)
            {
                _options = options;
            }
            Consumer();
        }
        public void Consumer()
        {
            var consumer = new Consumer(_options);
            consumer.ConsumeStart<dynamic>((a) =>
            {
                Console.WriteLine("Consumer1 收到数据：" + a);
            });
        }

        public void Consumer2()
		{
			var consumer = new Consumer(options2);
			consumer.ConsumeStart<dynamic>((a) => {
				Console.WriteLine("Consumer2 收到数据：" + a);
			});
		}

		public void Publisher()
        {
            var publisher = new Publisher(_options);
            publisher.PublisherStart(new { age=Console.ReadLine(),name=Console.ReadLine()});
        }
    }
}

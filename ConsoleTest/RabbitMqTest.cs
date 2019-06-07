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
        static RabbitMqOptions options = BaseCore.Configuration.GetSection("RabbitMqConfig").Get<RabbitMqOptions>();
        public void Consumer()
        {
            var consumer = new Consumer(options);
            consumer.ConsumeStart<dynamic>((a)=> {
                Console.WriteLine("收到数据："+a);
            });
        }

        public void Publisher()
        {
            var publisher = new Publisher(options);
            publisher.PublisherStart(new { age=Console.ReadLine(),name=Console.ReadLine()});
        }
    }
}

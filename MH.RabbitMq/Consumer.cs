using MH.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MH.RabbitMq
{

    //public class Consumer : BaseRabbitMQFactory
    //{
    //    private EventingBasicConsumer consumer;
    //    private string _consumerTag;
    //    private bool queueAck = false;
    //    public Consumer(RabbitMqOptions options) : base(options)
    //    {
    //        consumer = new EventingBasicConsumer(Channel);
    //        _consumerTag = Channel.BasicConsume(options.QueueName, options.QueueNoAck, consumer);
    //        queueAck = !options.QueueNoAck;
    //    }

    //    public void ConsumeStart<T>(Action<T> act)
    //    {
    //        consumer.Received += (m, e) =>
    //        {
    //            var msg = Encoding.UTF8.GetString(e.Body);
    //            if (!string.IsNullOrWhiteSpace(msg))
    //            {
    //                act(msg.ToObj<T>());
    //                if (queueAck)
    //                {
    //                    Channel.BasicAck(e.DeliveryTag, false);
    //                }
    //            }
    //        };
    //    }

    //    public R ConsumeStart<T, R>(Func<T, R> func)
    //    {
    //        R result = default(R);
    //        consumer.Received += (m, e) =>
    //        {
    //            var body = e.Body;
    //            var msg = Encoding.UTF8.GetString(body);
    //            if (!string.IsNullOrWhiteSpace(msg))
    //            {
    //                result = func(msg.ToObj<T>());
    //                if (queueAck)
    //                {
    //                    Channel.BasicAck(e.DeliveryTag, false);
    //                }
    //            }
    //        };
    //        return result;
    //    }

	public class Consumer : BaseRabbitMQFactory
	{
		private EventingBasicConsumer consumer;
		private string _consumerTag;
		private readonly object _lockObj = new object();
		public Consumer(RabbitMqOptions options) : base(options)
		{
			consumer = new EventingBasicConsumer(Channel);
			_consumerTag = Channel.BasicConsume(options.QueueName, options.QueueNoAck, consumer);
		}

		public void ConsumeStart<T>(Action<T> act)
		{
			consumer.Received += (m, e) =>
			{
				var msg = Encoding.UTF8.GetString(e.Body);
				if (!string.IsNullOrWhiteSpace(msg))
				{
					act(msg.ToObj<T>());
					if (_options.QueueNoAck)
						return;
					lock (_lockObj)
					{
						Channel.BasicAck(e.DeliveryTag, false);
					}
				}
			};
		}

		public R ConsumeStart<T, R>(Func<T, R> func)
		{
			R result = default(R);
			consumer.Received += (m, e) =>
			{
				var body = e.Body;
				var msg = Encoding.UTF8.GetString(body);
				if (!string.IsNullOrWhiteSpace(msg))
				{
					result = func(msg.ToObj<T>());

					if (_options.QueueNoAck)
						return;
					lock (_lockObj)
					{
						Channel.BasicAck(e.DeliveryTag, false);
					}
				}
			};
			return result;
		}

		public new void Dispose()
		{
			//consumer.OnCancel();
			if (Channel != null && !Channel.IsClosed)
			{
				Channel.BasicCancel(_consumerTag);
			}

			base.Dispose();
		}
		~Consumer()
		{
			Dispose();
		}
	}
}

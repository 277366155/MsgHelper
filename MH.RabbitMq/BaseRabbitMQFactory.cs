using RabbitMQ.Client;
using System;

namespace MH.RabbitMq
{
    public class BaseRabbitMQFactory : IDisposable
    {
        protected static RabbitMqOptions _options;
        private static ConnectionFactory _connFac;
        private static IConnection _connection;
        private static IModel _channel;
        public BaseRabbitMQFactory(RabbitMqOptions options)
        {
            _options = options;
            Initialize();
        }

        private static object LockConnObj = new object();
        public IConnection RabbitConnection
        {
            get {

                if (_connection == null)
                {
                    lock (LockConnObj)
                    {
                        if (_connFac == null)
                        {
                            _connFac = new ConnectionFactory()
                            {
                                RequestedHeartbeat = 30,
                                RequestedConnectionTimeout = 30000,
                                AutomaticRecoveryEnabled = true,

                                HostName = _options.HostName,
                                UserName = _options.UserName,
                                Password = _options.Password,
                                VirtualHost = _options.VirtualHost,
                                Port = _options.Port
                            };
                        }
                        if (_connection == null)
                        {
                            _connection = _connFac.CreateConnection();
                        }
                    }
                }
                return _connection;
            }
        }

        private static object LockChannelObj = new object();
        public IModel Channel
        {
            get
            {
                if (_channel == null)
                {
                    lock (LockChannelObj)
                    {
                        if (_channel == null)
                        {
                            _channel = RabbitConnection.CreateModel();
                        }
                    }
                }
                return _channel;
            }
        }
        protected  void Initialize()
        {
            Channel.ExchangeDeclare(_options.ExchangeName, _options.ExchangeType, _options.ExchangeDurable, _options.ExchangeAutoDelete, null);
            Channel.QueueDeclare(_options.QueueName, _options.QueueDurable, _options.QueueExclusive, _options.QueueAutoDelete, null);
        }

        public void Dispose()
        {
            if (Channel != null && !Channel.IsClosed)
            {
                Channel.Dispose();
            }
            if (RabbitConnection != null && RabbitConnection.IsOpen)
            {
                RabbitConnection.Dispose();
            }
        }
    }
}

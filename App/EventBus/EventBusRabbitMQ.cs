using Core.Data.Contract;
using Core.Data.Contract.EventBus;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IConnection _connection;
        private IModel _channel;
        protected IModel Channel
        {
            get
            {
                if (_channel == null)
                    _channel = _connection.CreateModel();

                return _channel;
            }
        }
        public EventBusRabbitMQ()
        {
            var _factory = new ConnectionFactory() { HostName = "192.168.0.107" };
            _connection = _factory.CreateConnection();

        }

        public void Publish<TMessage>(string nameExchange, string nameQueue, string routingKey, TMessage message) where TMessage : IMessageType
        {
            CreateExchange(nameExchange);

            CreateQueue(nameQueue, nameExchange, routingKey);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            Channel.BasicPublish(exchange: nameExchange,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);
        }

        private void CreateQueue(string queue, string exchange, string routingKey)
        {
            Channel.QueueDeclare(queue: $"{queue}.{routingKey}", exclusive: false) ;

            Channel.QueueBind(queue: $"{queue}.{routingKey}", exchange: exchange, routingKey: routingKey);
        }

        public void Subscribe<TMessage>(string queue, string exchange, string routingKey, Action<TMessage> callbackSubcribe) where TMessage : IMessageType
        {

            CreateExchange(exchange);

            CreateQueue(queue, exchange, routingKey);

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {

                var body = ea.Body.ToArray();
                string messageStr = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<TMessage>(messageStr);

                callbackSubcribe(message);

            };
            _channel.BasicConsume(queue: $"{queue}.{routingKey}",
                                 autoAck: true,
                                 consumer: consumer);
        }

        private void CreateExchange(string nameExchange)
        {
            Channel.ExchangeDeclare(exchange: nameExchange, type: ExchangeType.Topic);
        }
    }
}

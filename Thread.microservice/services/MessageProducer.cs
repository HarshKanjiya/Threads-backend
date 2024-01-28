using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace UserApi.microservice.services
{
    public class MessageProducer : IMessageProducer
    {
        public void SendingMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "user",
                VirtualHost = "/",
            };
            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare("user",durable:true,exclusive:true);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            { 
                var body = eventArgs.Body.ToArray();                 //  CZ, body is in byte array
                var message = Encoding.UTF8.GetString(body);

            };

            channel.BasicConsume("user", true, consumer);

        }
    }
}

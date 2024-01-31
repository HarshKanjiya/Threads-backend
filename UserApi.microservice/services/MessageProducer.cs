using System.Text;
using System.Text.Json;

namespace UserApi.microservice.services
{
    public class MessageProducer : IMessageProducer
    {
        public void SendingMessage<T>(T message)
        {
            /*  var factory = new ConnectionFactory()
              {
                  HostName = "localhost",
                  UserName = "user",
                  Password = "user",
                  VirtualHost = "/",
              };
              var connection = factory.CreateConnection();

              using var channel = connection.CreateModel();

              channel.QueueDeclare("user",durable:true,exclusive:true);

              var jsonString  = JsonSerializer.Serialize(message);
              var body = Encoding.UTF8.GetBytes(jsonString);

              channel.BasicPublish("", "user",body:body);*/

        }
    }
}

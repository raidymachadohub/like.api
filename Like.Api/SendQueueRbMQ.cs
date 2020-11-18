using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Api
{
    public class SendQueueRbMQ
    {
        public string Send(string messageObj)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "APILike",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                var body = Encoding.UTF8.GetBytes(messageObj);

                channel.BasicPublish(exchange: "",
                                     routingKey: "APILike",
                                     basicProperties: null,
                                     body: body);
            }
            return "Success";
        }
    }
}

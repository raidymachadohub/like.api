
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Like.RabbitMQ
{
    public class JSONParameter
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
    class Program
    {

        private static readonly AutoResetEvent _waitHandle =
            new AutoResetEvent(false);
        static void Main()
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;
                channel.BasicConsume(queue: "APILike",
                     autoAck: true,
                     consumer: consumer);
            }

            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Saindo...");

                // Libera a continuação da thread principal
                _waitHandle.Set();
                e.Cancel = true;
            };

            // Aguarda que o evento CancelKeyPress ocorra
            _waitHandle.WaitOne();


        }

        private static void Consumer_Received(
           object sender, BasicDeliverEventArgs e)
        {
            JSONParameter json = JsonConvert.DeserializeObject<JSONParameter>(Encoding.UTF8.GetString(e.Body.ToArray()));
            Console.WriteLine(JsonConvert.SerializeObject(json));
            //if (json.Type == "Like")
            //    _blogService.Like(json.Id);
            //else
            //    _blogService.UnLike(json.Id);
        }
    }
}

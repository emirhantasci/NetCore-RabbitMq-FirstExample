using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace NetCoreRabbitMQExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello - RabbitMQ Start");
            Customer customer = new Customer()
            {
                Name = "Emirhan",
                Surname = "Taşçı"
            };
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using(IConnection connection = factory.CreateConnection())
            using(IModel channel = connection.CreateModel())
            {
                Console.WriteLine("RabbitMQ Queue");
                channel.QueueDeclare(queue: "Test",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                string message = JsonConvert.SerializeObject(customer);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: "Test",
                    basicProperties: null,
                    body: body);
            }
            Console.WriteLine("RabbitMQ Finish");
            Console.ReadLine();
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

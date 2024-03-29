﻿
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
namespace FileSericeWorker.RabitMQ
{
    public class RabbitMQ
    {
    }
    public interface IRabitMqProducer
    {
        public void SendMessage<T>(T message);
    }
    public class RabitMqProducer : IRabitMqProducer
    {
        public void SendMessage<T>(T message)
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 3007
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();

            //Here we create channel with session and model
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("product", exclusive: false);

                //Serialize the message
                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);
                //put the data on to the product queue
                channel.BasicPublish(exchange: "", routingKey: "product", body: body);
               
            };
            //declare the queue after mentioning name and a few property related to that
            
        }
    }
}

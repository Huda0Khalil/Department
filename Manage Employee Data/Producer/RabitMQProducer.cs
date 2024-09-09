using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Manage_Employee_Data.Producer
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendEmployeeMessage<T>(T message)
        {
           
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare("EmployeeQueue", 
                durable: false,// Ensure the queue survives a broker restart
                exclusive: false,       // Make sure the queue is not exclusive to the connection
                autoDelete: false,
                 arguments: null);
            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //put the data on to the Employee queue
            channel.BasicPublish(exchange: "", routingKey: "EmployeeQueue", body: body);
        }
    }
}


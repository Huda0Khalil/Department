namespace Manage_Employee_Data.Send
{
    using Manage_Employee_Data.DTO;
    using RabbitMQ.Client;
    using System.Text;
    using System.Text.Json;

    public class RabbitMqSenderService
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "testQueue";
        private IConnection _connection;
        public RabbitMqSenderService()
        {
            CreateConnection();
        }
        private void CreateConnection()
        {
            var factory = new ConnectionFactory { HostName = _hostname, UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
        }
        public void SendMessage(List<EmployeeDataFromCSVfile> EmpDTOs)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                // Serialize the employee object to a JSON string
                var message = JsonSerializer.Serialize(EmpDTOs);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
                Console.WriteLine($"[x] Sent {message}");
            }
        }
    }
}

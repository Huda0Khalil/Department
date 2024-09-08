namespace Departments_Project.Receiver
{
    using AutoMapper;
    using Departments_Project.Entities;
    using Departments_Project.Entities.DTO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;
   // private readonly ILogger<RabbitMqListenerService> _logger;


    public class RabbitMqListenerService : IHostedService
    {
        private readonly IMapper _mapper;
        private IModel _channel;

        private readonly string _hostname = "localhost";
        private readonly string _queueName = "testQueue";
        private IConnection _connection;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RabbitMqListenerService(IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _mapper = mapper;
            CreateConnection();
            this._serviceScopeFactory = serviceScopeFactory;
        }
        private void CreateConnection()
        {
            var factory = new ConnectionFactory { HostName = _hostname,
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
        }
        public void StartListening(CancellationToken cancellationToken)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();

                    // Deserialize the JSON string back into a list of EmployeeDataFromCSVfile
                    var message = Encoding.UTF8.GetString(body);
                    var employeeDataFromCSV = JsonSerializer.Deserialize<List<EmployeeDataFromCSVfile>>(message);

                    // Start consuming messages only once, so remove this line from here:
                    // channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        // Ensure proper mapping of the deserialized employees
                        List<Employee> employees = _mapper.Map<List<Employee>>(employeeDataFromCSV);

                        // Add to the database and save changes
                        dbContext.Employees.AddRange(employees);
                        dbContext.SaveChanges();

                        Console.WriteLine($"[x] Received and saved {employees.Count} employees");
                    }
                };

                // This should be outside the consumer.Received event:
                channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

                // Keeping the application alive (but not inside the Received handler)
                Console.WriteLine("Listening for messages...");
                Console.ReadLine();  // Use this outside the event to keep the listener alive
            }
        } 

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => StartListening(cancellationToken), cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}

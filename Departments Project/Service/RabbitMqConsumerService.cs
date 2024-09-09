
using Departments_Project.Entities;
using Departments_Project.Entities.DTO;
using Departments_Project.Repository.EmployeeRepository;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Departments_Project.Service
{
    public class RabbitMqConsumerService : BackgroundService
    {
        private readonly IModel _channel;
        private readonly string _queueName = "EmployeeQueue";
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMqConsumerService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            Console.WriteLine("RabbitMqConsumerService initialized.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    Console.WriteLine("Message received.");

                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"Received JSON: {json}");

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var employeeRepository = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();

                        List<EmployeeDataFromCSVfile> employeeDto = JsonConvert.DeserializeObject<List<EmployeeDataFromCSVfile>>(json);

                        List<Employee> employees = employeeDto.Select(empDto => new Employee
                        {
                            Age = empDto.Age,
                            Name = empDto.Name,
                            PhoneNumber = empDto.PhoneNumber,
                            Email = empDto.Email,
                            DepartmentId = empDto.DepartmentId
                        }).ToList();

                        await employeeRepository.AddListEmployeesAsync(employees);

                        Console.WriteLine($"{employees.Count} employees added to the database.");
                    }

                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while processing message: {ex.Message}");
                }
            };


            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            // Close RabbitMQ channel and connection gracefully
            _channel.Close();
            await base.StopAsync(stoppingToken);
        }
    }
}


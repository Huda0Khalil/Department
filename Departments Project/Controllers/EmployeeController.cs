using AutoMapper;
using Departments_Project.CQRS.Commands.EmployeeCommand;
using Departments_Project.CQRS.Query;
using Departments_Project.CQRS.Query.EmployeeQuery;
using Departments_Project.Entities;
using Departments_Project.Entities.DTO;
using Departments_Project.Receiver;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Departments_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRabbitMqListenerService _rabbitMqListenerService;
        private readonly IMediator _mediator;
        private IMapper _mapper;
        public ApplicationDbContext Context { get; set; }

        public EmployeeController(IMediator mediator,IRabbitMqListenerService rabbitMqListenerService, ApplicationDbContext context)
        {
            _mediator = mediator;
            _rabbitMqListenerService = rabbitMqListenerService;
            Context = context;

        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand Command)
        {
            var employeeId = await _mediator.Send(Command);
            return Ok(employeeId);
        }
        [HttpGet]
        /*public IActionResult GetEmployees() {
            return RedirectToAction("AddListEmployee",{AddListEmployee Command);
        }*/
        [HttpGet]
        [Route("AddListEmployees")]
        public async Task<IActionResult> AddEmployeesfromSender()
        {
            
            var message = _rabbitMqListenerService.ReceiveMessage();
            if (string.IsNullOrEmpty(message))
            {
                return NotFound("No messages in the queue.");
            }

            // Deserialize the message into a list of employees
            var employeesData = JsonConvert.DeserializeObject<List<EmployeeDataFromCSVfile>>(message);

            // Map EmployeeDataFromCSVfile to Employee entity
            List<Employee> employees = new List<Employee>() ;
            if(employeesData != null) {
                foreach (var employeeData in employeesData)
                {
                    var employee = new Employee
                    {
                        Name = employeeData.Name,
                        Email = employeeData.Email,
                        PhoneNumber = employeeData.PhoneNumber,
                        Age = employeeData.Age,
                        DepartmentId = employeeData.DepartmentId
                    };
                    employees.Add(employee);
                }
            }
            // employees = _mapper.Map<List<Employee>>(employeeData);
            AddListEmployee Command = new AddListEmployee{ Employees = employees};
            var x = await _mediator.Send(Command);
            // Add employees to the database
            //Context.Employees.AddRange(employees);
            //await Context.SaveChangesAsync();

            return Ok($"{employees.Count} employees added to the database.");
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var query = new GetAllEmployeesQuery();
            var employees = await _mediator.Send(query);
            return Ok(employees);
        }
        [HttpGet]
        [Route("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            var query = new GetEmployeeByIdQuery(Id);
            var employee = await _mediator.Send(query);
            return Ok(employee);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(int id ,UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(command);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var command = new DeleteEmployeeCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}

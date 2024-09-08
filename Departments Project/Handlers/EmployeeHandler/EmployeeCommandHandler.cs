using MediatR;
using Departments_Project.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Departments_Project.Repository.EmployeeRepository;
using Departments_Project.CQRS.Commands.EmployeeCommand;

namespace Departments_Project.Handlers.EmployeeHandler
{
    public class EmployeeCommandHandler :
        IRequestHandler<CreateEmployeeCommand, int>,
        IRequestHandler<UpdateEmployeeCommand, Unit>,
        IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Age = request.Age,
                Email = request.Email,
                DepartmentId = request.DepartmentId
            };

            return await _repository.AddEmployeeAsync(employee);
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetEmployeeByIdAsync(request.Id);
            employee.Name = request.Name;
            employee.Age = request.Age;
            employee.PhoneNumber = request.PhoneNumber;
            employee.Email = request.Email;
            employee.DepartmentId = request.DepartmentId;

            await _repository.UpdateEmployeeAsync(employee);
            return Unit.Value;
        }
        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetEmployeeByIdAsync(request.Id);


            await _repository.DeleteEmployeeAsync(employee);
            return Unit.Value;
        }



    }

}

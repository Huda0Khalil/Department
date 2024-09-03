using Departments_Project.CQRS.Query.EmployeeQuery;
using Departments_Project.Entities;
using Departments_Project.Repository.EmployeeRepository;
using MediatR;

namespace Departments_Project.Handlers.EmployeeHandler
{
    public class EmployeeQueryHandler :
        IRequestHandler<GetEmployeeByIdQuery, Employee>,
        IRequestHandler<GetAllEmployeesQuery, List<Employee>>
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeQueryHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetEmployeeByIdAsync(request.Id);
        }

        public async Task<List<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllEmployeesAsync();
        }
    }
}

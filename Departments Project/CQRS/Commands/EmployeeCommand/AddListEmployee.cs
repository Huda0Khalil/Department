using Departments_Project.Entities;
using MediatR;

namespace Departments_Project.CQRS.Commands.EmployeeCommand
{
    public record AddListEmployee:IRequest<List<Employee>>
    {
        public required List<Employee> Employees {  get; set; }
    }
}

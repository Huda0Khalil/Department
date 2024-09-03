using Departments_Project.Entities;
using MediatR;
namespace Departments_Project.CQRS.Query.EmployeeQuery
{
    public record GetAllEmployeesQuery : IRequest<List<Employee>>
    {
        public List<Employee> employees { get; set; }
    }
}

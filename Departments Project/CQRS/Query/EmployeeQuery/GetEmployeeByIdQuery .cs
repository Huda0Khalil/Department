using Departments_Project.Entities;
using MediatR;
namespace Departments_Project.CQRS.Query.EmployeeQuery
{
    public record GetEmployeeByIdQuery : IRequest<Employee>
    {
        public int Id { get; set; }
        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}

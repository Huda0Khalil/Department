using MediatR;

namespace Departments_Project.CQRS.Commands.EmployeeCommand
{
    public record CreateEmployeeCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
    }
}

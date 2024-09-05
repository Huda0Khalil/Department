using Departments_Project.Entities;
using MediatR;

namespace Departments_Project.CQRS.Commands.EmployeeCommand
{
    public record UpdateEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public UpdateEmployeeCommand(int id, string firstName, string lastName, string email, int departmentId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DepartmentId = departmentId;
        }
    }

}

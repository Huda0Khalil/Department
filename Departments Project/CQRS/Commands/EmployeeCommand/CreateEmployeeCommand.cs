using MediatR;

namespace Departments_Project.CQRS.Commands.EmployeeCommand
{
    public record CreateEmployeeCommand : IRequest<int>
    {
        public string Name { get; set; }
       
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }
    }
}

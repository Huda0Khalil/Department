using Departments_Project.Entities;
using MediatR;

namespace Departments_Project.CQRS.Commands.EmployeeCommand
{
    public record UpdateEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }
        public UpdateEmployeeCommand(int id, string name, string email,string phoneNumber, int departmentId)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            DepartmentId = departmentId;
        }
    }

}

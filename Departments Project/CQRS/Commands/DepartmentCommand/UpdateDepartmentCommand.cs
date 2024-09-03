using Departments_Project.Entities;
using MediatR;

namespace Departments_Project.CQRS.Commands.DepartmentCommand
{
    public record UpdateDepartmentCommand: IRequest<Department>
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }


        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } 
        public UpdateDepartmentCommand(int departmentId, string name, string description, DateTime createdDate)
        {
            DepartmentId = departmentId;
            Name = name;
            Description = description;
            CreatedDate = createdDate;
        }
    }
}

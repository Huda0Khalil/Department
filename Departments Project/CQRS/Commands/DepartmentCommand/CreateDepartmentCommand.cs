using Departments_Project.Entities.DTO;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Departments_Project.CQRS.Commands.DepartmentCommand
{
    public record CreateDepartmentCommand : IRequest<int>
    {

        public int DepartmentId { get; set; }

        public string Name { get; set; }


        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        /*public CreateDepartmentCommand(DepartmentDTO departmentDTO)
        {
            Name = departmentDTO.Name;
            Description = departmentDTO.Description;
            CreatedDate = departmentDTO.CreatedDate;
        }*/
    }
}
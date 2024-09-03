using MediatR;

namespace Departments_Project.CQRS.Commands.DepartmentCommand
{
    public record DeleteDepartmentCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

using MediatR;

namespace Departments_Project.CQRS.Commands.EmployeeCommand
{
    public record DeleteEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

using Departments_Project.Entities;
using MediatR;

namespace Departments_Project.CQRS.Query.DepartmentQuery
{
    public record GetAllDepartmentQuery:IRequest<List<Department>>
    {
        //public required List<Department> Departments { get; set; }
       
    }
}

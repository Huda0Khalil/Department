using Departments_Project.Entities;
using MediatR;

namespace Departments_Project.CQRS.Query.DepartmentQuery
{
    public class GetDeparmentByIdQuery:IRequest<Department>
    {
        public int Id { get; set; }
        public GetDeparmentByIdQuery(int id)
        {
            Id = id;
        }
    }
}

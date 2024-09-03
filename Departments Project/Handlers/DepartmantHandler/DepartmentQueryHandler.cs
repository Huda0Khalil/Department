using Departments_Project.CQRS.Query.DepartmentQuery;
using Departments_Project.Entities;
using Departments_Project.Repository.DepartmentRepository;
using Departments_Project.Repository.EmployeeRepository;
using MediatR;

namespace Departments_Project.Handlers.DepartmantHandler
{
    public class DepartmentQueryHandler :
        IRequestHandler<GetDeparmentByIdQuery, Department>,
        IRequestHandler<GetAllDepartmentQuery,List<Department>>
    {
        public IDepartmentRepository _repository;
        public DepartmentQueryHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<Department> Handle(GetDeparmentByIdQuery request, CancellationToken cancellationToken)
        {
            return  await _repository.GetDepartmentById(request.Id);
            
        }

        public async Task<List<Department>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllDepartments();
        }
    }
}

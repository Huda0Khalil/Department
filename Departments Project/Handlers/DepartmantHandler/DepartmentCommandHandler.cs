using MediatR;
using Departments_Project.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Departments_Project.Repository.DepartmentRepository;
using Departments_Project.CQRS.Commands.DepartmentCommand;
using Departments_Project.CQRS.Commands.EmployeeCommand;
using Departments_Project.Repository.EmployeeRepository;
using Departments_Project.Entities.DTO;

namespace Departments_Project.Handlers.DepartmantHandler
{
    public class DepartmentCommandHandler :
        IRequestHandler<CreateDepartmentCommand, int>,
        IRequestHandler<UpdateDepartmentCommand, Department>,
        IRequestHandler<DeleteDepartmentCommand, Unit>
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentCommandHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if(request != null)
            {
                DepartmentDTO departmentDTO = new DepartmentDTO();
                departmentDTO.Name = request.Name;
                departmentDTO.Description = request.Description;
                //departmentDTO.CreatedDate = request.CreatedDate;
                return await _repository.AddDepartmentAsync(departmentDTO);
            }
          else return 0;
        }

        public async Task<Department> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department = await _repository.GetDepartmentById(request.DepartmentId);//_repository.GetDepartmentById(request.DepartmentId);
            if (department != null)
            {   DepartmentDTO departmentDTO = new DepartmentDTO();
                departmentDTO.DepartmentId = request.DepartmentId;
                departmentDTO.Name = request.Name;
                departmentDTO.Description = request.Description;
               var UpdetedDepartment = await _repository.UpdateDepartmentAsync(departmentDTO);
                return  UpdetedDepartment;
            }
            return  department;
            
        }
        public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
           // var department = await _repository.GetDepartmentById(request.Id);
           // if (department != null)
            {
                await _repository.DeleteDepartmentAsync(request.Id);
            }
            return Unit.Value;
        }
    }
}

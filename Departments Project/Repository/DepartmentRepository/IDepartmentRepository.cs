using Departments_Project.Entities;
using Departments_Project.Entities.DTO;

namespace Departments_Project.Repository.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        Task<int> AddDepartmentAsync(DepartmentDTO departmentDTO);

        Task DeleteDepartmentAsync(int id);
        Task<Department> GetDepartmentById(int id);

        Task<List<Department>> GetAllDepartments();
        Task<Department> UpdateDepartmentAsync(DepartmentDTO departmentDTO);
    }
}

using Departments_Project.Entities;
using Departments_Project.Entities.DTO;
using Microsoft.EntityFrameworkCore;

namespace Departments_Project.Repository.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private  ApplicationDbContext context;
        public DepartmentRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<int> AddDepartmentAsync(DepartmentDTO departmentDTO)
        {
            if (departmentDTO != null)
            {
                Department department = new Department();
                department.Name = departmentDTO.Name;
                department.Description = departmentDTO.Description;
                
                context.Departments.Add(department);
                await context.SaveChangesAsync();
                return department.DepartmentId;
            }
            return 0;
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            Department? dep = context.Departments.Where(x => x.DepartmentId == id).SingleOrDefault();
            //if find dep in DB
            if (dep != null)
            {
                context.Departments.Remove(dep);
                await context.SaveChangesAsync();
            }
        }
        public async Task<List<Department>> GetAllDepartments()
        {
            List<Department> departments = context.Departments.Include(x => x.Employees).ToList();
            return departments;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            Department department = await context.Departments.Where(x => x.DepartmentId == id).SingleOrDefaultAsync();
            var x = 0;
            return department;
        }

        public async Task<Department> UpdateDepartmentAsync( DepartmentDTO departmentDTO)
        {
            Department? dep = context.Departments.Where(d => d.DepartmentId == departmentDTO.DepartmentId).FirstOrDefault();
            //if find dep in DB
            if (dep != null)
            {
                dep.Name = departmentDTO.Name;
                dep.Description = departmentDTO.Description;
                context.Departments.Update(dep);
                await context.SaveChangesAsync();

            }
            return dep;
        }
    }
}

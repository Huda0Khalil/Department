using Departments_Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace Departments_Project.Repository.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.Id;
        }


        public async Task DeleteEmployeeAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = _context.Employees.Include(x => x.Department).ToList();
            return employees;

        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            Employee? employee = await _context.Employees.Include(x => x.Department).ThenInclude(x =>x.Employees).Where(x => x.Id == id).SingleOrDefaultAsync();
            return employee;
        }


        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
    }
}

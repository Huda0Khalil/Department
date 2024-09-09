using Departments_Project.Entities;

namespace Departments_Project.Repository.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        Task<int> AddEmployeeAsync(Employee employee);
        Task<List<Employee>> AddListEmployeesAsync(List<Employee> Employees);
        Task DeleteEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> UpdateEmployeeAsync(Employee employee);
    }
}

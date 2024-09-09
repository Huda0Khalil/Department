using Manage_Employee_Data.DTO;

namespace Manage_Employee_Data.Services
{
    public interface IEmployeeService
    {
        public EmployeeDataFromCSVfile AddProduct(List<EmployeeDataFromCSVfile> EmployeesData);
    }
}

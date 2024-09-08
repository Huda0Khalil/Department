namespace Departments_Project.Entities.DTO
{
    public class EmployeeDataFromCSVfile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public int? DepartmentId { get; set; }
    }
}

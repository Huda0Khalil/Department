using System.ComponentModel.DataAnnotations;

namespace Departments_Project.Entities
{
      public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public int Age { get; set; }
            public int? DepartmentId { get; set; }
            public Department? Department { get; set; }
       
            
    }
    
}

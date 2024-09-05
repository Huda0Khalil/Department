using System.ComponentModel.DataAnnotations;

namespace Departments_Project.Entities
{
      public class Employee
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public int? DepartmentId { get; set; }
            public Department? Department { get; set; }
       
            
    }
    
}

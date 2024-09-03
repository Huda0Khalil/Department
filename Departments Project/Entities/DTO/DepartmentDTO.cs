using System.ComponentModel.DataAnnotations;

namespace Departments_Project.Entities.DTO
{
    public class DepartmentDTO
    {
      
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } 
    }
}

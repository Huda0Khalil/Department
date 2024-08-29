using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Departments_Project.Entities
{
    public class Department
    {
        [Key]
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}

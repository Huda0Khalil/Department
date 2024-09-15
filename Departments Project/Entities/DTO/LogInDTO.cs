using System.ComponentModel.DataAnnotations;

namespace Departments_Project.Entities.DTO
{
    public class LogInDTO
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}

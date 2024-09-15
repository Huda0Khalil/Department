using System.ComponentModel.DataAnnotations;

namespace Manage_Employee_Data.DTO
{
    public class LogInDTO
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}

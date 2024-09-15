using System.ComponentModel.DataAnnotations;

namespace Departments_Project.Entities.DTO
{
    public class NewAccountDTO
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Email { get; set; }

    }
}

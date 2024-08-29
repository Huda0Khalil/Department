using Departments_Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace Departments_Project
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<Department> departments { get; set; }
    }
}

using Departments_Project.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Departments_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        ApplicationDbContext context;
        public DepartmentController(ApplicationDbContext _context)
        {
            context = _context;
        }
        [HttpPost]
        public ActionResult creat(Department department)
        { 
            if (department != null)
            {
                context.departments.Add(department);
                context.SaveChanges();

            }
            return Ok(department);

        }
        [HttpGet]
        public ActionResult read()
        {
            List<Department> departments = context.departments.ToList();
            return Ok(departments);

        }
        [HttpPut]
        public ActionResult update(int id, Department department)
        {
            Department dep = context.departments.Where(d => d.DepartmentId == id).FirstOrDefault();
            //if find dep in DB
            if (dep != null)
            {
                dep.Name = department.Name;
                dep.Description = department.Description;
                context.departments.Update(dep);
                context.SaveChanges();
                return Ok(dep);
            }
            //if didn't find dep in DB
                return NotFound();
            
        }
        [HttpDelete]
        public ActionResult delete(int id)
        {
            Department dep = context.departments.Where(d => d.DepartmentId == id).FirstOrDefault();
            //if find dep in DB
            if (dep != null)
            {
                context.departments.Remove(dep);
                context.SaveChanges();
                return Ok(dep);
            }
            //if didn't find dep in DB
            return NotFound();

        }
    }

    
}

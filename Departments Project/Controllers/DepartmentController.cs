using Departments_Project.CQRS.Commands.DepartmentCommand;
using Departments_Project.CQRS.Commands.EmployeeCommand;
using Departments_Project.CQRS.Query.DepartmentQuery;
using Departments_Project.Entities;
using Departments_Project.Entities.DTO;
using Departments_Project.Repository.DepartmentRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Departments_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
          _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult> CreatDepartment(DepartmentDTO departmentDTO)
        {
            CreateDepartmentCommand command = new CreateDepartmentCommand
            {
                Description = departmentDTO.Description,
                Name = departmentDTO.Name,
               // CreatedDate = DateTime.Now
            };
               var departmentId = await _mediator.Send(command);
                return Ok(departmentId);           
        }
        [HttpGet]
        public async Task<ActionResult> GetAllDepartments()
        {
            //
            var Query = new GetAllDepartmentQuery(); 
            List<Department> departments = await _mediator.Send(Query);
            return Ok(departments);

        }
        [HttpPut]
        public async Task<ActionResult> UpdateDepartment(UpdateDepartmentCommand command)
        {
            //
            var dep = await _mediator.Send(command);
            if(dep != null) {
                return Ok(dep);
            }
            //if didn't find dep in DB
                return NotFound();
            
        }
        [HttpDelete]
        public ActionResult DeleteDepartment(int id)
        {
            var command = new DeleteDepartmentCommand { Id = id };
            _mediator.Send(command);
            return Ok();
         }
    }

    
}

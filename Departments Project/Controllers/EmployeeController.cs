﻿using AutoMapper;
using Departments_Project.CQRS.Commands.EmployeeCommand;
using Departments_Project.CQRS.Query;
using Departments_Project.CQRS.Query.EmployeeQuery;
using Departments_Project.Entities;
using Departments_Project.Entities.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Departments_Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IMapper _mapper;
        public ApplicationDbContext Context { get; set; }

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpPost]
        [Route("AddEmployee")]

        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand Command)
        {
            var employeeId = await _mediator.Send(Command);
            return Ok(employeeId);
        }
        
       
        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var query = new GetAllEmployeesQuery();
            var employees = await _mediator.Send(query);
            return Ok(employees);
        }
        [HttpGet]
        [Route("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            var query = new GetEmployeeByIdQuery(Id);
            var employee = await _mediator.Send(query);
            return Ok(employee);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(int id ,UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(command);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var command = new DeleteEmployeeCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}

using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Manage_Employee_Data.DTO;
using System.Xml.Linq;
using Manage_Employee_Data.Producer;
using Microsoft.AspNetCore.Authorization;

namespace Manage_Employee_Data.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadDocumentsController : ControllerBase
    {
        private readonly IRabitMQProducer _rabbitMqProducer;

        private readonly string csvFilePath = "Employee.csv";
        public UploadDocumentsController(IRabitMQProducer rabbitMqProducer)
        {
            this._rabbitMqProducer = rabbitMqProducer;
        }

        [HttpPost("Upload-csvFile")]
        public IActionResult UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            else
            {
                try
                {
                    List<EmployeeDataFromCSVfile> Records ;
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                    {
                         Records = csv.GetRecords<EmployeeDataFromCSVfile>().ToList();
                    }
                    _rabbitMqProducer.SendEmployeeMessage(Records);
                    return Ok(Records);
                }
                catch (IOException ex)
                {
                    return StatusCode(500, $"Error reading file: {ex.Message}");
                }



            }
        }
        
    }

    
}

using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Manage_Employee_Data.DTO;
using System.Xml.Linq;

namespace Manage_Employee_Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadDocumentsController : ControllerBase
    {
        private readonly string csvFilePath = "Employee.csv";
        [HttpPost("Upload-csvFile")]
        public IActionResult UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            else
            {
                try
                {
                    List<EmployeeDataFromCSVfile> Records = new List<EmployeeDataFromCSVfile>();
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                    {
                         Records = csv.GetRecords<EmployeeDataFromCSVfile>().ToList();
                    }
                    return Ok(Records);
                }
                catch (IOException ex)
                {
                    return StatusCode(500, $"Error reading file: {ex.Message}");
                }



            }
        }
        [HttpPut("Edit")]
        public IActionResult EditCsv([FromBody] EmployeeDataFromCSVfile Emp)
        {
            /*if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            else*/
            {
                var records = new List<EmployeeDataFromCSVfile>();
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    records = csv.GetRecords<EmployeeDataFromCSVfile>().ToList();
                }
                var recordToEdit = records.FirstOrDefault(r => r.Name == Emp.Name);
                if (recordToEdit != null)
                {
                    recordToEdit.PhoneNumber = Emp.PhoneNumber; // Edit the desired field
                    recordToEdit.Email = Emp.Email;
                    recordToEdit.Age = Emp.Age;
                    recordToEdit.DepartmentId = Emp.DepartmentId;
                }
                using (var writer = new StreamWriter(csvFilePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
                return Ok(csvFilePath);
            }
        }
        [HttpPost("Add Employee")]
        public async Task<IActionResult> AddToCsv([FromBody] EmployeeDataFromCSVfile Emp)
        {
            if (Emp == null)
                return BadRequest("Invalid record provided.");

            /*if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            else*/
            try
            {
                using (var writer = new StreamWriter(csvFilePath, append: true))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.WriteRecord(Emp);
                    await writer.FlushAsync();
                }
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            return Ok("Record added successfully.");
        }
        [HttpDelete("Delete Employee")]
        public async Task<IActionResult> DeleteFromCsv([FromBody] String _Name ) {
            if (string.IsNullOrEmpty(_Name))
                return BadRequest("Invalid name provided.");
            List<EmployeeDataFromCSVfile> records;
            //To read records from csv file
            try
            {
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    records = csv.GetRecords<EmployeeDataFromCSVfile>().ToList();
                }
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Error reading file: {ex.Message}");
            }
            // Find and remove the record by name
            var recordToRemove = records.FirstOrDefault(r => r.Name == _Name);
            if (recordToRemove == null)
                return NotFound("Record not found.");

            records.Remove(recordToRemove);
            // Rewrite the CSV file with the remaining records
            try
            {
                using (var writer = new StreamWriter(csvFilePath, false)) // Overwrite the file
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.WriteRecords(records);
                    await writer.FlushAsync();
                }
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Error writing file: {ex.Message}");
            }

            return Ok("Record deleted successfully.");

        }
    }

    
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dotnet_Project.Common.Model;

namespace Dotnet_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dotnet_ProjectController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Position = "Software Engineer" },
            new Employee { Id = 2, Name = "Jane Smith", Position = "Project Manager" },
             new Employee { Id = 3, Name = "Jimin", Position= "Developer"},
            new Employee { Id = 4, Name = "Anuroopa", Position= "Developer"}
        };

        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(employees);
        }


        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest("Employee data is required.");  

           
            employee.Id = employees.Count > 0 ? employees.Max(e => e.Id) + 1 : 1;

            employees.Add(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
            
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound($"Bad Request! Employee with Id {id} not found.");  

            return Ok(employee);  
        }


   
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound($"Employee with Id {id} not found.");  

           
            employee.Name = updatedEmployee.Name;
            employee.Position = updatedEmployee.Position;

            return Ok(employee); 
        }

       
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound($"Employee with Id {id} not found.");  

            employees.Remove(employee);

            return Ok($"Employee with Id {id} deleted successfully.");  
        }


        [HttpDelete("bulk")]
        public IActionResult DeleteMultipleEmployees([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return BadRequest("Please provide at least one Id.");

            var toDelete = employees.Where(e => ids.Contains(e.Id)).ToList();

            if (toDelete.Count == 0)
                return NotFound("No employees found with the given Ids.");

            foreach (var emp in toDelete)
                employees.Remove(emp);

            return Ok($"{toDelete.Count} employee(s) deleted successfully.");
        }
    }
}

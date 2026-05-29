using Dotnet_Project.Common.Model;
using Dotnet_Project.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// API to insert new employee
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> InsertEmployee(EmployeeRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var employee = new Employee
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    Username = request.Username,

                    PasswordHash =
                        BCrypt.Net.BCrypt.HashPassword(request.Password),

                    RoleId = request.RoleId,
                    IsActive = request.IsActive
                };

                bool result =
                    await _employeeService.InsertEmployee(employee);

                if (result)
                {
                    return Ok("Employee inserted successfully");
                }

                return BadRequest("Employee insert failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// API to get all employees
        /// </summary>
        [Authorize(Roles = "Manager,DIRECTOR")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees =
                    await _employeeService.GetEmployees();

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// API to get employee by id
        /// </summary>
        [Authorize(Roles = "Admin,Manager,HR")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee =
                    await _employeeService.GetEmployeeById(id);

                if (employee == null)
                {
                    return NotFound("Employee not found");
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// API to update employee
        /// </summary>
        [Authorize(Roles = "Admin,Manager,DIRECTOR")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool result =
                    await _employeeService.UpdateEmployee(employee);

                if (result)
                {
                    return Ok("Employee updated successfully");
                }

                return BadRequest("Employee update failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// API to delete employee
        /// </summary>
        [Authorize(Roles = "Admin,DIRECTOR")]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                bool result =
                    await _employeeService.DeleteEmployee(id);

                if (result)
                {
                    return Ok("Employee deleted successfully");
                }

                return BadRequest("Employee delete failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
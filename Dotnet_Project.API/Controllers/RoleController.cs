using Dotnet_Project.Common.Model;
using Dotnet_Project.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
       /// <summary>
       /// API to insert new role
       ///</summary>

        [HttpPost]
        public async Task<IActionResult> InsertRole(Role role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool result =
                    await _roleService.InsertRole(role);

                if (result)
                {
                    return Ok("Role inserted successfully");
                }

                return BadRequest("Insert failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// API to get all roles
        ///</summary>

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles =
                    await _roleService.GetRoles();

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// API to get role by ID
        ///</summary>
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(string roleId)
        {
            try
            {
                var role =
                    await _roleService.GetRoleById(roleId);

                if (role == null)
                {
                    return NotFound("Role not found");
                }

                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// API to update role
        ///</summary>

        [HttpPut]
        public async Task<IActionResult> UpdateRole(Role role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool result =
                    await _roleService.UpdateRole(role);

                if (result)
                {
                    return Ok("Role updated successfully");
                }

                return BadRequest("Update failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// API to delete role
        ///</summary>
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            try
            {
                bool result =
                    await _roleService.DeleteRole(roleId);

                if (result)
                {
                    return Ok("Role deleted successfully");
                }

                return BadRequest("Delete failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
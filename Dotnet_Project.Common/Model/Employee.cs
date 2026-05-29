using System.ComponentModel.DataAnnotations;

namespace Dotnet_Project.Common.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        public string? RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
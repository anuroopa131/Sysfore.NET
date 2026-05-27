using System.ComponentModel.DataAnnotations;

namespace Dotnet_Project.Common.Model
{
    public class Role
    {
        public string? RoleId { get; set; }

        [Required]
        public string? RoleName { get; set; }
    }
}
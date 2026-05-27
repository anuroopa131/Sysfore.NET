using System.ComponentModel.DataAnnotations;

namespace Dotnet_Project.Common.Model
{
    public class Employee
    {
       public int Id { get; set; }  
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Position { get; set; }
    }
}

using Dotnet_Project.Common.Model;

namespace Dotnet_Project.Service.Abstractions
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployees();

        Task<Employee> GetEmployeeById(int id);

        Task<bool> InsertEmployee(Employee employee);

        Task<bool> UpdateEmployee(Employee employee);

        Task<bool> DeleteEmployee(int id);
    }
}
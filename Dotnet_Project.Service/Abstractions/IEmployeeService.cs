using Dotnet_Project.Common.Model;

namespace Dotnet_Project.Service.Abstractions;

public interface IEmployeeService
{
    Task<bool> InsertEmployee(Employee employee);

    Task<IEnumerable<Employee>> GetEmployees();

    Task<Employee?> GetEmployeeById(int id);

    Task<bool> UpdateEmployee(Employee employee);

    Task<bool> DeleteEmployee(int id);

    Task<Employee?> GetByUsername(string username);
}
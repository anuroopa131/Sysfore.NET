using Dotnet_Project.Common.Model;


namespace Dotnet_Project.Store.Abstractions
{
    public interface IEmployeeStore
    {
        Task<Employee?> GetByUsername(string username); 
        Task<bool> InsertEmployee(Employee employee);
        Task<List<Employee>> GetEmployees();
        Task<Employee?> GetEmployeeById(int employeeId);
        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(int employeeId);
    }
}
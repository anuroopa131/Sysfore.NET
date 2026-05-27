using Dotnet_Project.Common.Model;
using Dotnet_Project.Service.Abstractions;
using Dotnet_Project.Store.Abstractions;

namespace Dotnet_Project.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeStore _employeeStore;

        public EmployeeService(IEmployeeStore employeeStore)
        {
            _employeeStore = employeeStore;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeStore.GetEmployees();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeStore.GetEmployeeById(id);
        }

        public async Task<bool> InsertEmployee(Employee employee)
        {
            return await _employeeStore.InsertEmployee(employee);
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            return await _employeeStore.UpdateEmployee(employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeStore.DeleteEmployee(id);
        }
    }
}
using Dotnet_Project.Common.Model;
using Dotnet_Project.Service.Abstractions;
using Dotnet_Project.Store.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Dotnet_Project.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeStore _employeeStore;

        public EmployeeService(IEmployeeStore employeeStore)
        {
            _employeeStore = employeeStore;
        }

        /// <summary>
        /// Service method to insert employee
        /// </summary>
        /// 
        public async Task<Employee?> GetByUsername(string username)
        {
            return await _employeeStore.GetByUsername(username);
        }
        public async Task<bool> InsertEmployee(Employee employee)
        {
            return await _employeeStore.InsertEmployee(employee);
        }

        /// <summary>
        /// Service method to get all employees
        /// </summary>
        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeStore.GetEmployees();
        }

        /// <summary>
        /// Service method to get employee by id
        /// </summary>
        public async Task<Employee?> GetEmployeeById(int employeeId)
        {
            return await _employeeStore.GetEmployeeById(employeeId);
        }

        /// <summary>
        /// Service method to update employee
        /// </summary>
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            return await _employeeStore.UpdateEmployee(employee);
        }

        /// <summary>
        /// Service method to delete employee
        /// </summary>
        public async Task<bool> DeleteEmployee(int employeeId)
        {
            return await _employeeStore.DeleteEmployee(employeeId);
        }
    }
}
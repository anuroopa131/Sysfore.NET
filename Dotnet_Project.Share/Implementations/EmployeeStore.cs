using Dapper;
using Dotnet_Project.Common.Constants;
using Dotnet_Project.Common.Model;
using Dotnet_Project.Store.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Dotnet_Project.Store.Implementations
{
    public class EmployeeStore : IEmployeeStore
    {
        private readonly string _connectionString;

        public EmployeeStore(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Store method to insert employee
        /// </summary>
        public async Task<Employee?> GetByUsername(string username)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Username", username);

            return await connection.QueryFirstOrDefaultAsync<Employee>(
                SqlConstants.GetEmployeeByUsername,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        
        public async Task<bool> InsertEmployee(Employee employee)
        {
            try
            {
                using var connection =
                    new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();

                parameters.Add(ParameterConstants.FullName, employee.FullName);
                parameters.Add(ParameterConstants.Email, employee.Email);
                parameters.Add(ParameterConstants.Username, employee.Username);
                parameters.Add(ParameterConstants.PasswordHash, employee.PasswordHash);
                parameters.Add(ParameterConstants.RoleId, employee.RoleId);

                int rowsAffected =
                    await connection.ExecuteAsync(
                        SqlConstants.InsertEmployee,
                        parameters,
                        commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Store method to get all employees
        /// </summary>
        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                using var connection =
                    new SqlConnection(_connectionString);

                var employees =
                    await connection.QueryAsync<Employee>(
                        SqlConstants.GetEmployees,
                        commandType: CommandType.StoredProcedure);

                return employees.ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Store method to get employee by id
        /// </summary>
        public async Task<Employee?> GetEmployeeById(int employeeId)
        {
            try
            {
                using var connection =
                    new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();

                parameters.Add(
                    ParameterConstants.EmployeeId,
                    employeeId);

                var employee =
                    await connection.QueryFirstOrDefaultAsync<Employee>(
                        SqlConstants.GetEmployeeById,
                        parameters,
                        commandType: CommandType.StoredProcedure);

                return employee;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Store method to update employee
        /// </summary>
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                using var connection =
                    new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();

                parameters.Add(ParameterConstants.EmployeeId, employee.EmployeeId);
                parameters.Add(ParameterConstants.FullName, employee.FullName);
                parameters.Add(ParameterConstants.Email, employee.Email);
                parameters.Add(ParameterConstants.Username, employee.Username);
                parameters.Add(ParameterConstants.PasswordHash, employee.PasswordHash);
                parameters.Add(ParameterConstants.RoleId, employee.RoleId);
                parameters.Add(ParameterConstants.IsActive, employee.IsActive);

                int rowsAffected =
                    await connection.ExecuteAsync(
                        SqlConstants.UpdateEmployee,
                        parameters,
                        commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Store method to delete employee
        /// </summary>
        public async Task<bool> DeleteEmployee(int employeeId)
        {
            try
            {
                using var connection =
                    new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();

                parameters.Add(
                    ParameterConstants.EmployeeId,
                    employeeId);

                int rowsAffected =
                    await connection.ExecuteAsync(
                        SqlConstants.DeleteEmployee,
                        parameters,
                        commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
            catch
            {
                throw;
            }
        }
    }
}
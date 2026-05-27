using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

using Dotnet_Project.Common.Constants;
using Dotnet_Project.Common.Model;
using Dotnet_Project.Store.Abstractions;

namespace Dotnet_Project.Store.Implementations
{
    public class EmployeeStore : IEmployeeStore
    {
        private readonly IConfiguration _configuration;

        public EmployeeStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                using SqlConnection connection =
                    new SqlConnection(
                        _configuration.GetConnectionString("DefaultConnection"));

                var result = await connection.QueryAsync<Employee>(
                    SqlConstants.GetEmployees,
                    commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                using SqlConnection connection =
                    new SqlConnection(
                        _configuration.GetConnectionString("DefaultConnection"));

                var parameters = new DynamicParameters();

                parameters.Add(ParameterConstants.Id, id);

                var result =
                    await connection.QueryFirstOrDefaultAsync<Employee>(
                        SqlConstants.GetEmployeeById,
                        parameters,
                        commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> InsertEmployee(Employee employee)
        {
            try
            {
                using SqlConnection connection =
                    new SqlConnection(
                        _configuration.GetConnectionString("DefaultConnection"));

                var parameters = new DynamicParameters();

                parameters.Add(ParameterConstants.Name, employee.Name);

                parameters.Add(ParameterConstants.Position, employee.Position);

                int rowsAffected = await connection.ExecuteAsync(
                    SqlConstants.InsertEmployee,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                using SqlConnection connection =
                    new SqlConnection(
                        _configuration.GetConnectionString("DefaultConnection"));

                var parameters = new DynamicParameters();

                parameters.Add(ParameterConstants.Id, employee.Id);

                parameters.Add(ParameterConstants.Name, employee.Name);

                parameters.Add(ParameterConstants.Position, employee.Position);

                int rowsAffected = await connection.ExecuteAsync(
                    SqlConstants.UpdateEmployee,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                using SqlConnection connection =
                    new SqlConnection(
                        _configuration.GetConnectionString("DefaultConnection"));

                var parameters = new DynamicParameters();

                parameters.Add(ParameterConstants.Id, id);

                int rowsAffected = await connection.ExecuteAsync(
                    SqlConstants.DeleteEmployee,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
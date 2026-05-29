using Dapper;
using Dotnet_Project.Common.Constants;
using Dotnet_Project.Common.Model;
using Dotnet_Project.Store.Abstractions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dotnet_Project.Store.Implementations;

public class EmployeeStore : IEmployeeStore
{
    private readonly string _connectionString;

    public EmployeeStore(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Connection string missing");
    }

    public async Task<bool> InsertEmployee(Employee employee)
    {
        using var connection = new SqlConnection(_connectionString);

        var parameters = new DynamicParameters();

        parameters.Add(ParameterConstants.FullName, employee.FullName);
        parameters.Add(ParameterConstants.Email, employee.Email);
        parameters.Add(ParameterConstants.Username, employee.Username);
        parameters.Add(ParameterConstants.PasswordHash, employee.PasswordHash);
        parameters.Add(ParameterConstants.RoleId, employee.RoleId);
        parameters.Add(ParameterConstants.IsActive, employee.IsActive);

        int rows = await connection.ExecuteAsync(
            SqlConstants.InsertEmployee,
            parameters,
            commandType: CommandType.StoredProcedure);

        return rows > 0;
    }

    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        using var connection = new SqlConnection(_connectionString);

        return await connection.QueryAsync<Employee>(
            SqlConstants.GetEmployees,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Employee?> GetEmployeeById(int id)
    {
        using var connection = new SqlConnection(_connectionString);

        var parameters = new DynamicParameters();

        parameters.Add(ParameterConstants.EmployeeId, id);

        return await connection.QueryFirstOrDefaultAsync<Employee>(
            SqlConstants.GetEmployeeById,
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateEmployee(Employee employee)
    {
        using var connection = new SqlConnection(_connectionString);

        var parameters = new DynamicParameters();

        parameters.Add(ParameterConstants.EmployeeId, employee.EmployeeId);
        parameters.Add(ParameterConstants.FullName, employee.FullName);
        parameters.Add(ParameterConstants.Email, employee.Email);
        parameters.Add(ParameterConstants.Username, employee.Username);
        parameters.Add(ParameterConstants.PasswordHash, employee.PasswordHash);
        parameters.Add(ParameterConstants.RoleId, employee.RoleId);
        parameters.Add(ParameterConstants.IsActive, employee.IsActive);

        int rows = await connection.ExecuteAsync(
            SqlConstants.UpdateEmployee,
            parameters,
            commandType: CommandType.StoredProcedure);

        return rows > 0;
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        using var connection = new SqlConnection(_connectionString);

        var parameters = new DynamicParameters();

        parameters.Add(ParameterConstants.EmployeeId, id);

        int rows = await connection.ExecuteAsync(
            SqlConstants.DeleteEmployee,
            parameters,
            commandType: CommandType.StoredProcedure);

        return rows > 0;
    }

    public async Task<Employee?> GetByUsername(string username)
    {
        using var connection = new SqlConnection(_connectionString);

        var parameters = new DynamicParameters();

        parameters.Add("@Username", username);

        return await connection.QueryFirstOrDefaultAsync<Employee>(
            SqlConstants.GetEmployeeByUsername,
            parameters,
            commandType: CommandType.StoredProcedure);
    }
}
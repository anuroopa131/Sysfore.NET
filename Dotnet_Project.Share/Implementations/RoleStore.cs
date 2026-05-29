using Dapper;
using Dotnet_Project.Common.Constants;
using Dotnet_Project.Common.Model;
using Dotnet_Project.Store.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Dotnet_Project.Store.Implementations
{
    public class RoleStore : IRoleStore
    {
        private readonly string _connectionString;

        public RoleStore(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Store method to insert role
        /// </summary>
        public async Task<bool> InsertRole(Role role)
        {
            try
            {
                using var connection =
                    new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();

                parameters.Add(
                    ParameterConstants.RoleName,
                    role.RoleName);

                int rowsAffected =
                    await connection.ExecuteAsync(
                        SqlConstants.InsertRole,
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
        /// Store method to get all roles
        /// </summary>
        public async Task<List<Role>> GetRoles()
        {
            try
            {
                using var connection =
                    new SqlConnection(_connectionString);

                var roles =
                    await connection.QueryAsync<Role>(
                        SqlConstants.GetRoles,
                        commandType: CommandType.StoredProcedure);

                return roles.ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Store method to get role by id
        /// </summary>
        public async Task<Role?> GetRoleById(string roleId)
        {
            try
            {
                using var connection =
                    new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();

                parameters.Add(
                    ParameterConstants.RoleId,
                    roleId);

                var role =
                    await connection.QueryFirstOrDefaultAsync<Role>(
                        SqlConstants.GetRoleById,
                        parameters,
                        commandType: CommandType.StoredProcedure);

                return role;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Store method to update role
        /// </summary>
        public async Task<bool> UpdateRole(Role role)
        {
            try
            {
                using var connection =
                    new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();

                parameters.Add(
                    ParameterConstants.RoleId,
                    role.RoleId);

                parameters.Add(
                    ParameterConstants.RoleName,
                    role.RoleName);

                int rowsAffected =
                    await connection.ExecuteAsync(
                        SqlConstants.UpdateRole,
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
        /// Store method to delete role
        /// </summary>
        public async Task<bool> DeleteRole(string roleId)
        {
            try
            {
                using var connection =
                    new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();

                parameters.Add(
                    ParameterConstants.RoleId,
                    roleId);

                int rowsAffected =
                    await connection.ExecuteAsync(
                        SqlConstants.DeleteRole,
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
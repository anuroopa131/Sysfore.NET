using Dotnet_Project.Common.Model;

namespace Dotnet_Project.Service.Abstractions
{
    public interface IRoleService
    {
        Task<bool> InsertRole(Role role);

        Task<List<Role>> GetRoles();

        Task<Role?> GetRoleById(string roleId);

        Task<bool> UpdateRole(Role role);

        Task<bool> DeleteRole(string roleId);
    }
}
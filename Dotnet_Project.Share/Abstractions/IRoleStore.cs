using Dotnet_Project.Common.Model;


namespace Dotnet_Project.Store.Abstractions
{
    public interface IRoleStore
    {
        Task<bool> InsertRole(Role role);

        Task<List<Role>> GetRoles();

        Task<Role?> GetRoleById(string roleId);

        Task<bool> UpdateRole(Role role);

        Task<bool> DeleteRole(string roleId);
    }
}
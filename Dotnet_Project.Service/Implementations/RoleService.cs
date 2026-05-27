using Dotnet_Project.Common.Model;
using Dotnet_Project.Service.Abstractions;
using Dotnet_Project.Store.Abstractions;

namespace Dotnet_Project.Service.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleStore _roleStore;

        public RoleService(IRoleStore roleStore)
        {
            _roleStore = roleStore;
        }

        public async Task<bool> InsertRole(Role role)
        {
            return await _roleStore.InsertRole(role);
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _roleStore.GetRoles();
        }

        public async Task<Role?> GetRoleById(string roleId)
        {
            return await _roleStore.GetRoleById(roleId);
        }

        public async Task<bool> UpdateRole(Role role)
        {
            return await _roleStore.UpdateRole(role);
        }

        public async Task<bool> DeleteRole(string roleId)
        {
            return await _roleStore.DeleteRole(roleId);
        }
    }
}
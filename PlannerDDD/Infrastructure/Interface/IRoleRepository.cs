using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interface
{
    public interface IRoleRepository
    {
        // The function to get role object based on role id
        public Task<RoleDetail> GetRoleObjectBasedOnId(int roleId);

        // The function get all role assignments in the system
        public Task<List<RoleDetailUserProfile>> GetAllRoleAssigmments();
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Interface
{
    public interface IRoleService
    {
        // The function to add role to a user with specified user id
        public Task<bool> AddRoleToAUser(int userId, int roleId);

        // The function to remove role from a user
        public Task<bool> RemoveRoleFromAUser(int roleDetailUserProfileId);

        // The function to get all role assignments in the system
        public Task<List<RoleDetailUserProfile>> GetAllRoleAssignments();
    }
}

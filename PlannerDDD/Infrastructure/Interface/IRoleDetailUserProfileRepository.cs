using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interface
{
    public interface IRoleDetailUserProfileRepository
    {
        // The function to get role detail user profile object based on id
        public Task<RoleDetailUserProfile> GetRoleDetailUserProfileBasedOnId(int roleDetailUserProfileId);
    }
}

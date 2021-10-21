using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class RoleRepository : RepositoryBase<RoleDetail>, IRoleRepository
    {
        // The database context
        private readonly EFContext _databaseContext;

        public RoleRepository(EFContext databaseContext) : base(databaseContext)
        {
            // Initialize database context
            _databaseContext = databaseContext;
        }

        // The function to get role object based on id
        public async Task<RoleDetail> GetRoleObjectBasedOnId(int roleId)
        {
            // Reference the database to get role object of the role that will be assigned to the user
            // Also map it to the role view model
            var roleObject = await _databaseContext.RoleDetails
                .Include(roleDetail => roleDetail.Role)
                .FirstOrDefaultAsync(roleDetail => roleDetail.Id == roleId);

            // Return the obtained role object
            return roleObject;
        }

        // The function to get all role assignments in the system
        public async Task<List<RoleDetailUserProfile>> GetAllRoleAssigmments()
        {
            // Reference the database to get list of role details and include role object as well
            var roleDetails = await _databaseContext.RoleDetailUserProfiles
                .Include(roleDetail => roleDetail.RoleDetail).ThenInclude(roleDetail => roleDetail.Role)
                .Include(roleDetail => roleDetail.UserProfile)
                .ToListAsync();

            // Return list of role assignments
            return roleDetails;
        }
    }
}

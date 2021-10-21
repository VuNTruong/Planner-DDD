using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class RoleDetailUserProfileRepository : RepositoryBase<RoleDetailUserProfile>, IRoleDetailUserProfileRepository
    {
        // The database context
        private readonly EFContext _databaseContext;

        public RoleDetailUserProfileRepository(EFContext databaseContext) : base(databaseContext)
        {
            // Initialize database context
            _databaseContext = databaseContext;
        }

        public async Task<RoleDetailUserProfile> GetRoleDetailUserProfileBasedOnId(int roleDetailUserProfileId)
        {
            // Reference the database to get role detail user profile object which is going to be removed
            var roleDetailUserProfileObject = await _databaseContext.RoleDetailUserProfiles
                .Where(r => r.Id == roleDetailUserProfileId)
                .Include(r => r.UserProfile.User)
                .Include(r => r.RoleDetail.Role)
                .FirstOrDefaultAsync();

            // Return the object
            return roleDetailUserProfileObject;
        }
    }
}

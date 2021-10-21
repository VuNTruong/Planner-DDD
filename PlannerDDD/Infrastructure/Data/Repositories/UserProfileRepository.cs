using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Infrastructure.Interface;

namespace Infrastructure.Data.Repositories
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        // User id of the currently logged in user
        private readonly string currentUserId;

        // The database context
        private readonly EFContext _databaseContext;

        // Constructor which will initialize database context from the base class
        public UserProfileRepository(EFContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            // Http context accessor is injected in here via DI
            // Get user id of the currently logged in user
            currentUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Initialize database context
            _databaseContext = dbContext;
        }

        // The function to get user id of the currently logged in user
        public async Task<int> GetCurrentUserId()
        {
            // Reference the database, include user identity object as well
            var currentUserObject = await _databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .FirstOrDefaultAsync(userProfile => userProfile.User.Id == currentUserId);

            // Return the obtained user id
            return currentUserObject.Id;
        }

        // The function to get user object of the currently logged in user
        public async Task<UserProfile> GetCurrentUserObject()
        {
            // Reference the database, include user identity object as well
            var currentUserObject = await _databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .FirstOrDefaultAsync(userProfile => userProfile.User.Id == currentUserId);

            // Return the obtained user object
            return currentUserObject;
        }

        // The function to get user object based on id
        public async Task<UserProfile> GetUserObjectBasedOnId(int userId)
        {
            // Reference the database to get user object of the user based on id
            var userObject = await _databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .FirstOrDefaultAsync(userProfile => userProfile.Id == userId);

            // Return the obtained user object
            return userObject;
        }

        // The function to get user object based on email
        public async Task<UserProfile> GetUserObjectBasedOnEmail(string userEmail)
        {
            // Reference the database to get user object of the user based on email
            var userObject = await _databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .FirstOrDefaultAsync(userProfile => userProfile.User.Email == userEmail);

            // Return the obtained user object
            return userObject;
        }
    }
}

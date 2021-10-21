using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Interface.Shared;

namespace Infrastructure.Interface
{
    public interface IUserProfileRepository : IAsyncRepository<UserProfile>
    {
        // The function to get numeric user id of the currently logged in user
        public Task<int> GetCurrentUserId();

        // The function to get user object of the currently logged in user
        public Task<UserProfile> GetCurrentUserObject();

        // The function to get user object of the user with specified user id
        public Task<UserProfile> GetUserObjectBasedOnId(int userId);

        // The function to get user object of the user with specified email
        public Task<UserProfile> GetUserObjectBasedOnEmail(string userEmail);
    }
}

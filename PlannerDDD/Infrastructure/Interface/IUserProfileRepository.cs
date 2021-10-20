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
    }
}

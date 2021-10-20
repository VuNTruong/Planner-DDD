using Domain.Entities;
using Interface.Shared;

namespace Interface
{
    public interface IUserProfileRepository : IAsyncRepository<UserProfile>
    {
    }
}

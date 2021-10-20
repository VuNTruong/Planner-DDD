using Domain.Entities;
using Interface;

namespace Infrastructure.Data.Repositories
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        // Constructor which will initialize database context from the base class
        public UserProfileRepository(EFContext dbContext) : base(dbContext)
        { }
    }
}

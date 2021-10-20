using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Interface.Shared;
using Interface;

namespace Services
{
    public class UserService : BaseService, IUserService
    {
        // User profile repository
        private readonly IUserProfileRepository _userProfileRepository;

        // Constructor
        public UserService(IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository) : base(unitOfWork)
        {
            // Initialize user profile repository
            _userProfileRepository = userProfileRepository;
        }

        // The function to get user object of the currently logged in user
        public async Task<UserProfile> GetUserObjectOfCurrentUser()
        {
            // Call the function to get user object of the currently logged in user
            var currentUserObject = await _userProfileRepository.GetCurrentUserObject();

            // Return the user object
            return currentUserObject;
        }
    }
}

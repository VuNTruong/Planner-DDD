using System.Threading.Tasks;
using Domain.Entities;
using Interface;
using Interface.Shared;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class AuthService : BaseService
    {
        // User profile repository
        private IUserProfileRepository _userProfileRepository;

        // User manager and sign in manager
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        // Constructor
        public AuthService(IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository,
            SignInManager<User> signInManager, UserManager<User> userManager) : base(unitOfWork)
        {
            // Initialize user profile repository
            _userProfileRepository = userProfileRepository;

            // Initialize sign in manager
            _signInManager = signInManager;

            // Initialize user manager
            _userManager = userManager;
        }

        // The function to sign up a new account
        public async Task<bool> SignUp(string fullName, string email, string password)
        {
            // Create user profile async repository
            var userProfileAsyncRepo = UnitOfWork.AsyncRepository<UserProfile>();

            // Call the function to create new user profile object in the database and return the
            // created one
            var createdUserProfileObject = await userProfileAsyncRepo
                .AddAsync(new UserProfile(fullName));

            // Save changes
            await UnitOfWork.SaveChangesAsync();

            // Create new user object
            var newUser = new User(createdUserProfileObject.Id, email);

            // Perform sign up operation and get the result
            var result = await _userManager.CreateAsync(newUser, password);

            // Return result
            return result.Succeeded;
        }

        // The function to sign in
        public async Task<bool> SignIn(string email, string password)
        {
            // Call the function to sign the user in and get the result
            var result = await _signInManager.PasswordSignInAsync(email, password, true, false);

            // Return result
            return result.Succeeded;
        }
    }
}

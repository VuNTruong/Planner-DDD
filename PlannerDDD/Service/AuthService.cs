using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Interface.Shared;
using Interface;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class AuthService : BaseService, IAuthService
    {
        // User manager and sign in manager
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        // User profile repository
        private readonly IUserProfileRepository _userProfileRepository;

        // Send email service
        private readonly ISendEmailService _sendEmailService;

        // Constructor
        public AuthService(IUnitOfWork unitOfWork,
            SignInManager<User> signInManager, UserManager<User> userManager, IUserProfileRepository userProfileRepository,
            ISendEmailService sendEmailService) : base(unitOfWork)
        {
            // Initialize sign in manager
            _signInManager = signInManager;

            // Initialize user manager
            _userManager = userManager;

            // Initialize user profile repository
            _userProfileRepository = userProfileRepository;

            // Initialize send email service
            _sendEmailService = sendEmailService;
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

        // The function to sign out
        public async Task SignOut()
        {
            // Call the function to sign the user out and get the result
            await _signInManager.SignOutAsync();
        }

        // The function to send password reset email to the user with specified email
        public async Task<string> SendPasswordResetEmail(string email)
        {
            // Call the function to get user object based on email
            var userObject = await _userProfileRepository.GetUserObjectBasedOnEmail(email);

            // If user object is null, it means that user entered the email that does not exist
            if (userObject == null) {
                return "No such email";
            }

            // Call the function to get password reset token for the user
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(userObject.User);

            // Send email with password reset token
            await _sendEmailService.SendEmailAsync(email, "Reset password", $"Use this token to reset your password {passwordResetToken}");

            // Return result
            return "Email sent";
        }
    }
}

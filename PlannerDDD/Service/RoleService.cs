using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Interface.Shared;
using Interface;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class RoleService : BaseService, IRoleService
    {
        // User manager and sign in manager
        private readonly UserManager<User> _userManager;

        // User profile repository
        private readonly IUserProfileRepository _userProfileRepository;

        // Role repository
        private readonly IRoleRepository _roleRepository;

        // Role detail user profile repository
        private readonly IRoleDetailUserProfileRepository _roleDetailUserProfileRepository;

        // Constructor
        public RoleService(IUnitOfWork unitOfWork, UserManager<User> userManager,
            IUserProfileRepository userProfileRepository, IRoleRepository roleRepository,
            IRoleDetailUserProfileRepository roleDetailUserProfileRepository) : base(unitOfWork)
        {
            // Initialize user manager and sign in manager
            _userManager = userManager;

            // Initialize user profile repository
            _userProfileRepository = userProfileRepository;

            // Initialize role repository
            _roleRepository = roleRepository;

            // Initialize role detail user profile repository
            _roleDetailUserProfileRepository = roleDetailUserProfileRepository;
        }

        // The function to add role to a user
        public async Task<bool> AddRoleToAUser(int userId, int roleId)
        {
            // Create the role detail user profile async repository
            var userProfileAsyncRepo = UnitOfWork.AsyncRepository<RoleDetailUserProfile>();

            // Call the function to get user object of the user with specified user id
            var userObject = await _userProfileRepository.GetUserObjectBasedOnId(userId);

            // Call the function to get role object of the role with specified role id
            var roleObject = await _roleRepository.GetRoleObjectBasedOnId(roleId);

            // Create new record in role detial user profile table to assign role to a user
            await userProfileAsyncRepo.AddAsync(new RoleDetailUserProfile
            {
                RoleDetailId = roleObject.Id,
                UserProfileId = userObject.Id
            });

            // Save changes
            await UnitOfWork.SaveChangesAsync();

            // Assign role to a user and get the result
            var result = await _userManager.AddToRoleAsync(userObject.User, roleObject.Role.Name);

            // Return result
            return result.Succeeded;
        }

        // The function to remove role from a user
        public async Task<bool> RemoveRoleFromAUser(int roleDetailUserProfileId)
        {
            // Create role detail role detail user profile async repository
            var roleDetailUserProfileAsyncRepo = UnitOfWork.AsyncRepository<RoleDetailUserProfile>();

            // Call the function to get user profile role detail object which is going to be removed
            var roleDetailUserProfileObjectToRemove = await _roleDetailUserProfileRepository.GetRoleDetailUserProfileBasedOnId(roleDetailUserProfileId);

            // Delete the role detail user profile object associated with the role which need to be removed from the user
            await roleDetailUserProfileAsyncRepo.DeleteAsync(roleDetailUserProfileObjectToRemove);

            // Save changes
            await UnitOfWork.SaveChangesAsync();

            // Remove role from a user and get the result
            IdentityResult removeRoleResult = await _userManager.RemoveFromRolesAsync(roleDetailUserProfileObjectToRemove.UserProfile.User,
                new List<string> { roleDetailUserProfileObjectToRemove.RoleDetail.Role.Name });

            // Return result
            return removeRoleResult.Succeeded;
        }

        // The function to get all role assignments in the system
        public async Task<List<RoleDetailUserProfile>> GetAllRoleAssignments()
        {
            // Call the function to get all role assignments in the system
            var roleAssignments = await _roleRepository.GetAllRoleAssigmments();

            // Return list of role assignments
            return roleAssignments;
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlannerDDD.ViewModels;

namespace PlannerDDD.Controllers
{
    [ApiController]
    [Route("api/v1/Role")]
    [Authorize(Roles = "Admin")]
    public class RoleController
    {
        // Role service
        private readonly IRoleService _roleService;

        // Auto mapper
        private readonly IMapper _mapper;

        // Constructor
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            // Initialize role service
            _roleService = roleService;

            // Initialize auto mapper
            _mapper = mapper;
        }

        // The function to add new role to a user
        [HttpPost("addRoleToAUser")]
        public async Task<JsonResult> AddRoleToUser([FromBody]RoleUserViewModel roleUserViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to add role to a user
            var result = await _roleService.AddRoleToAUser(roleUserViewModel.UserId, roleUserViewModel.RoleId);

            // Add data to the response data
            if (result)
            {
                responseData.Add("status", "Done");
                responseData.Add("data", "Role has been assigned to a user");
            } else
            {
                responseData.Add("status", "Not done");
                responseData.Add("data", "There seems to be an error");
            }

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to remove role from a user
        [HttpPost("removeRoleFromAUser")]
        public async Task<JsonResult> RemoveRoleFromAUser(int roleDetailUserProfileId)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to remove role from a user
            var result = await _roleService.RemoveRoleFromAUser(roleDetailUserProfileId);

            // Add data to the response data
            if (result)
            {
                responseData.Add("status", "Done");
                responseData.Add("data", "Role has been removed from a user");
            } else
            {
                responseData.Add("status", "Not done");
                responseData.Add("data", "Role was not removed from a user");
            }

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to get all role assignments in the system
        [HttpGet("getAllRoleAssignments")]
        public async Task<JsonResult> GetAllRoleAssignments()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to get all role assignments in the system
            var roleAssignments = await _roleService.GetAllRoleAssignments();

            // Map list of role detail user profile to list of role assignment view models
            var roleAssignmentViewModels = _mapper.Map<List<RoleAssignmentViewModel>>(roleAssignments);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", roleAssignmentViewModels);

            // Return response to the client
            return new JsonResult(responseData);
        }
    }
}

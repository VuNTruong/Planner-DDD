using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Interface;
using Microsoft.AspNetCore.Mvc;
using PlannerDDD.ViewModels;

namespace PlannerDDD.Controllers
{
    [ApiController]
    [Route("api/v1/User")]
    public class UserController
    {
        // User service
        private readonly IUserService _userService;

        // Auto mapper
        private readonly IMapper _mapper;

        // Constructor
        public UserController(IUserService userService, IMapper mapper)
        {
            // Initialize user service
            _userService = userService;

            // Initialize auto mapper
            _mapper = mapper;
        }

        // The function to get user object of the currently logged in user
        [HttpGet("getInfoOfCurrentUser")]
        public async Task<JsonResult> GetCurrentUserObject()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to get user object of the currently logged in user
            var currentUserObject = await _userService.GetUserObjectOfCurrentUser();

            // Map user object into user view model
            var currentUserViewModel = _mapper.Map<UserViewModel>(currentUserObject);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", currentUserViewModel);

            // Return response to the client
            return new JsonResult(responseData);
        }
    }
}

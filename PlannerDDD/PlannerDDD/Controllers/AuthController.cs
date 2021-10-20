﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlannerDDD.ViewModels.Auth;
using Services;

namespace PlannerDDD.Controllers
{
    [ApiController]
    [Route("api/v1/Auth")]
    public class AuthController
    {
        // Auth service
        private readonly AuthService _authService;

        // Constructor
        public AuthController(AuthService authService)
        {
            // Initialize auth service
            _authService = authService;
        }

        // The function to sign up a new account
        [HttpPost("SignUp")]
        public async Task<JsonResult> SignUp([FromBody] SignUpViewModel signUpViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to sign a user up
            var signUpResult = await _authService.SignUp(signUpViewModel.FullName, signUpViewModel.Email, signUpViewModel.Password);

            // Add data to the response data
            if (signUpResult)
            {
                responseData.Add("status", "Done");
                responseData.Add("data", "Account has been created");
            } else
            {
                responseData.Add("status", "Not done");
                responseData.Add("data", "There seems to be an error");
            }

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to sign in
        [HttpPost("SignIn")]
        public async Task<JsonResult> SignIn([FromBody] SignInViewModel signInViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to sign a user in
            var signInResult = await _authService.SignIn(signInViewModel.Email, signInViewModel.Password);

            // Add data to the response data
            if (signInResult)
            {
                responseData.Add("status", "Done");
                responseData.Add("data", "You are signed in");
            } else
            {
                responseData.Add("status", "Not done");
                responseData.Add("data", "There seems to be an error");
            }

            // Return the response
            return new JsonResult(responseData);
        }
    }
}
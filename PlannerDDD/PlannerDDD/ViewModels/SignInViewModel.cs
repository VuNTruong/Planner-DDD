using System;
using System.ComponentModel.DataAnnotations;

namespace PlannerDDD.ViewModels
{
    public class SignInViewModel
    {
        [Required]
        // Sign in email
        public string Email { get; set; }

        [Required]
        // Sign in password
        public string Password { get; set; }
    }
}

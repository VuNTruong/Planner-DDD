using System;
using System.ComponentModel.DataAnnotations;

namespace PlannerDDD.ViewModels.Auth
{
    public class SignUpViewModel
    {
        [Required]
        // Sign up email
        public string Email { get; set; }

        [Required]
        // Sign up full name
        public string FullName { get; set; }

        [Required]
        // Sign up password
        public string Password { get; set; }

        [Compare("Password")]
        // Sign up password confirm
        public string PasswordConfirm { get; set; }
    }
}

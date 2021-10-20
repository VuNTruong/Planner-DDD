using System;
using System.Threading.Tasks;

namespace Interface
{
    public interface IAuthService
    {
        // The function to sign up a new account
        public Task<bool> SignUp(string fullName, string email, string password);

        // The function to sign in
        public Task<bool> SignIn(string email, string password);

        // The function to sign out
        public Task SignOut();
    }
}

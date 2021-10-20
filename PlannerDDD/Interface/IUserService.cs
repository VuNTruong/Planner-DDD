using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Interface
{
    public interface IUserService
    {
        // The function to get user object of the currently logged in user
        public Task<UserProfile> GetUserObjectOfCurrentUser();
    }
}

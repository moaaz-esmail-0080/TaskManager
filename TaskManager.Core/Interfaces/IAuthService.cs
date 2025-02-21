using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequest model); // Registers a new user
        Task<AuthTokenResponse> LoginAsync(LoginRequest model); // Logs in a user and returns a token
        Task<TaskUser> GetUserByIdAsync(string id); // Fetches a user by ID
        Task<TaskUser> GetUserByEmailAsync(string email); // Fetches a user by email
        Task<TaskUser> GetUserByUsernameAsync(string username); // Fetches a user by username
        Task<bool> UpdateUserAsync(UpdateUserRequest model); // Updates user information
        Task<bool> DeactivateUserAsync(string id); // Deactivates a user account
    }
}

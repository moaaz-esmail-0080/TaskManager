using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Entities
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; } // New field to capture user's full name
        public string Role { get; set; }     // Optional: Role (e.g., Admin, User, etc.)

        public RegisterRequest(string email, string password, string fullName, string role)
        {
            Email = email;
            Password = password;
            FullName = fullName;
            Role = role;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Entities
{
    public class LoginRequest
    {
        public string Email { get; set; } // User's email address for login
        public string Password { get; set; } // User's password for authentication
        public bool RememberMe { get; set; } // Whether to keep the user logged in across sessions
    }
}

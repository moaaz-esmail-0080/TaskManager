using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Entities
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; } // Optional: Add full name field if relevant
        public string Role { get; set; }     // Optional: Add role for managing user roles

    }
}
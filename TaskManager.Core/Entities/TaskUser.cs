using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Entities
{
    public class TaskUser : IdentityUser
    {
        // Additional properties related to task management can be added here
        public bool IsActive { get; set; } = true; // Indicates if the user is active
        public string FirstName { get; set; } // First name of the user
        public string LastName { get; set; } // Last name of the user


        // Navigation property to track projects the user is associated with
        public ICollection<Task> Projects { get; set; } = new List<Task>();

        public TaskUser() : base() { }

        // Optionally add custom methods for user-related functionality
        public string GetFullName() => $"{FirstName} {LastName}";
    }
}
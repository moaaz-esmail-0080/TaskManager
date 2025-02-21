using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Entities
{
    public class TaskRole : IdentityRole
    {
        // Additional properties can be added if needed
        // For example, you could have custom properties like "Description" or "Permissions" for a task-based role system

        public string Description { get; set; } // A description for the role

        public TaskRole() : base() { }

        public TaskRole(string roleName) : base(roleName) { }

        // Optionally add custom methods to manage role-specific functionality
    }
}

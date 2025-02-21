using Microsoft.EntityFrameworkCore; // Required for async methods
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Infra.Context;
using Task = TaskManager.Core.Entities.Task;

namespace TaskManager.Infra.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        // Constructor to inject AppDbContext
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all tasks
        public async Task<List<Task>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync(); // Query to get all tasks
        }

        // Get task by ID
        public async Task<Task?> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id); // Find task by ID
        }

        // Add a new task
        public async Task<int> AddTaskAsync(Task task)
        {
            _context.Tasks.Add(task); // Add the task to the context
            await _context.SaveChangesAsync(); // Save changes to the database
            return task.Id; // Return the task ID
        }

        // Update an existing task
        public async Task<bool> UpdateTaskAsync(Task task)
        {
            _context.Tasks.Update(task); // Mark task as modified
            return await _context.SaveChangesAsync() > 0; // Save changes and return success status
        }

        // Delete a task by ID
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id); // Find task by ID
            if (task == null) return false; // If task not found, return false

            _context.Tasks.Remove(task); // Remove the task from the context
            return await _context.SaveChangesAsync() > 0; // Save changes and return success status
        }
    }
}

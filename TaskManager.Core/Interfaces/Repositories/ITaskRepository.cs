using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities;
using Task = TaskManager.Core.Entities.Task;

namespace TaskManager.Core.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Task>> GetAllTasksAsync();
        Task<Task?> GetTaskByIdAsync(int id);
        Task<int> AddTaskAsync(Entities.Task task);
        Task<bool> UpdateTaskAsync(Entities.Task task);
        Task<bool> DeleteTaskAsync(int id);
    }
}

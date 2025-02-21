using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskManager.Application.DTOs;
using Task = TaskManager.Core.Entities.Task;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;  // Inject AutoMapper

        // Constructor to inject repository and AutoMapper
        public TaskController(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        // Get all tasks
        [HttpGet]
        public async Task<ActionResult<List<TaskDTO>>> GetTasks()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            var taskDTOs = _mapper.Map<List<TaskDTO>>(tasks);  // Map to DTO
            return Ok(taskDTOs);  // Return 200 OK with DTOs
        }

        // Get task by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> GetTask(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();  // Return 404 if task is not found

            var taskDTO = _mapper.Map<TaskDTO>(task);  // Map to DTO
            return Ok(taskDTO);  // Return 200 OK with DTO
        }

        // Add a new task
        [HttpPost]
        public async Task<ActionResult> AddTask([FromBody] TaskDTO taskDTO)
        {
            if (taskDTO == null)
                return BadRequest();  // Return 400 if taskDTO is null

            var task = _mapper.Map<Task>(taskDTO);  // Map DTO to Entity
            var taskId = await _taskRepository.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = taskId }, taskDTO);  // Return 201 Created with the task location
        }

        // Update an existing task
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(int id, [FromBody] TaskDTO taskDTO)
        {
            if (taskDTO == null || taskDTO.Id != id)
                return BadRequest();  // Return 400 if taskDTO is null or IDs don't match

            var task = _mapper.Map<Task>(taskDTO);  // Map DTO to Entity
            var updated = await _taskRepository.UpdateTaskAsync(task);
            if (!updated)
                return NotFound();  // Return 404 if task is not found

            return NoContent();  // Return 204 No Content (successful update with no data)
        }

        // Delete a task by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var deleted = await _taskRepository.DeleteTaskAsync(id);
            if (!deleted)
                return NotFound();  // Return 404 if task is not found

            return NoContent();  // Return 204 No Content (successful delete with no data)
        }
    }
}

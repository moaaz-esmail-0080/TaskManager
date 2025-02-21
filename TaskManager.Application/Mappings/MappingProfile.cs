using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define entity-to-DTO mappings
            CreateMap<Task, TaskDTO>(); // Task entity to TaskDTO
            CreateMap<TaskDTO, Task>(); // TaskDTO to Task entity (for Create/Update operations)
        }
    }
}

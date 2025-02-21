using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Queries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Repositories;
using Task = TaskManager.Core.Entities.Task;

namespace TaskManager.Application.Handlers
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, Core.Entities.Task?>
    {
        private readonly ITaskRepository _repository;

        public GetTaskByIdHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<Task?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetTaskByIdAsync(request.Id);
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Repositories;

namespace TaskManager.Application.Handlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly ITaskRepository _repository;

        public CreateTaskHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new Core.Entities.Task
            {
                Title = request.Title,
                Description = request.Description
            };

            return await _repository.AddTaskAsync(task);
        }
    }
}

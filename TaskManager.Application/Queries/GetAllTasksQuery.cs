﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities;

namespace TaskManager.Application.Queries
{
    public class GetAllTasksQuery : IRequest<List<Core.Entities.Task>>
    {
    }
}

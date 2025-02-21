﻿using System.Net;

namespace TaskManager.Core.Exceptions;

public class NotFoundException(string resource, object key) : BaseException($"The resource '{resource}' with key '{key}' was not found.")
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}

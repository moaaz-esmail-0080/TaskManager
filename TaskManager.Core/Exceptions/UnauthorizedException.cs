using System.Net;

namespace TaskManager.Core.Exceptions;

public class UnauthorizedException(string message) : BaseException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
}

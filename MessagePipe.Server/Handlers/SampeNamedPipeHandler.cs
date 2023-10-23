using System;
namespace MessagePipe.Server.Handlers;

// example: server handler
public class SampeNamedPipeHandler : IAsyncRequestHandler<int, string>
{
    public async ValueTask<string> InvokeAsync(int request, CancellationToken cancellationToken = default)
    {
        await Task.Delay(1);
        if (request == -1)
        {
            throw new Exception("NO -1");
        }
        else
        {
            return "ECHO:" + request.ToString();
        }
    }
}

using MessagePipe;
using MessagePipe.Interprocess.Workers;
using MessagePipe.Server.Handlers;
using MessagePipe.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

// Add MessagePipe for interprocess communication.
builder.Services.AddMessagePipe();

/* In this code, we'll explore various methods of using Message Pipes in .NET, 
 * including TcpInterprocess and NamedPipeInterprocess but alternatively it can be configured to use redis server */

// Register a TCP pipe.
builder.Services.AddMessagePipeTcpInterprocess("127.0.0.1", 3215, config =>
{
    config.InstanceLifetime = InstanceLifetime.Singleton;
    config.HostAsServer = true;
});

// Register a named pipe (commented out, as an example).
//builder.Services.AddMessagePipeNamedPipeInterprocess("sample-pipe", config =>
//{
//    config.InstanceLifetime = InstanceLifetime.Singleton;
//    config.HostAsServer = true; // Should be set to true for server-side.
//});

// Register a request handler.
builder.Services.AddSingleton<IAsyncRequestHandler<int, string>, SampleRemoteHandler>();




var app = builder.Build();

var scope = app.Services.CreateAsyncScope();

// Depending on your configuration, choose either TcpWorker or NamedPipeWorker.

// Get a TCPWorker.
using var pipeworker = app.Services.GetRequiredService<TcpWorker>();

// Get a NamedPipeWorker (commented out, as an example).
//using var pipeworker = app.Services.GetRequiredService<NamedPipeWorker>(); // Initialize NamedPipeWorker

//Start worker
pipeworker.StartReceiver();

app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be established through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

using MessagePipe;
using MessagePipe.Interprocess.Workers;
using MessagePipe.Server.Handlers;
using MessagePipe.Server.Services;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddMessagePipe();
builder.Services.AddSingleton<IAsyncRequestHandler<int, string>, SampeNamedPipeHandler>();

builder.Services.AddMessagePipeTcpInterprocess("127.0.0.1", 3215, config =>
{
    config.InstanceLifetime = InstanceLifetime.Singleton;
    config.HostAsServer = true; // Should be true for server side.
});

//builder.Services.AddMessagePipeNamedPipeInterprocess("sample-pipe", config =>
//{
//    config.InstanceLifetime = InstanceLifetime.Singleton;
//    config.HostAsServer = true; // Should be true for server side.
//});


var app = builder.Build();

var scope = app.Services.CreateAsyncScope();

using var pipeworker = app.Services.GetRequiredService<TcpWorker>(); // Initialize NamedPipeWorker

//using var pipeworker = app.Services.GetRequiredService<NamedPipeWorker>(); // Initialize NamedPipeWorker

//using var pipeworker = scope.ServiceProvider.GetRequiredService<TcpWorker>(); // Initialize NamedPipeWorker
pipeworker.StartReceiver();


 
// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
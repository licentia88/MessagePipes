using MessagePipe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add MessagePipe services for interprocess communication.
builder.Services.AddMessagePipe();

// Configure and register a TCP MessagePipe for interprocess communication.
builder.Services.AddMessagePipeTcpInterprocess("127.0.0.1", 3215);

/* Uncomment the following line if using Named Pipe instead of TCP.
 * Alternatively make the required changes in the server side */

// builder.Services.AddMessagePipeNamedPipeInterprocess("sample-pipe");

var app = builder.Build();

// Wait for the server to run (not necessary in production).
await Task.Delay(5000);




if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

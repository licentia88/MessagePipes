using MessagePack;
using Microsoft.AspNetCore.Components;

namespace MessagePipe.Web.Pages;

public partial class Index
{
    [Inject]
    public required IRemoteRequestHandler<int, string> remoteRequestHandler { get; set; }


    [Inject]
    public required IPublisher<string,int> publisher { get; set; }
 

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
 
        await remoteRequestHandler.InvokeAsync(999);

        //the subscriber for this publisher is defined in app.razor.cs 
        publisher.Publish("FromIndexPage", 2746);
    }


}



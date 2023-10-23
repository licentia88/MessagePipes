using System;
using Microsoft.AspNetCore.Components;

namespace MessagePipe.Web.Pages;

public partial class Index
{
    [Inject]
    public required IRemoteRequestHandler<int, string> publisher { get; set; }



    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        try
        {
            await publisher.InvokeAsync(999);

        }
        catch (Exception ex)
        {
            Console.WriteLine();
        }

        Console.WriteLine();
    }
}



using Microsoft.AspNetCore.Components;

namespace MessagePipe.Web;

public partial class App
{
    [Inject]
    public required ISubscriber<string, int> subscriber { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        subscriber.Subscribe("FromIndexPage", Console.WriteLine);
    }
}


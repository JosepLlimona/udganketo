using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalRApp.Hubs;

public class UdgAnketoHub : Hub
{
    public async Task Answer(string name, string answerUuid) 
    {
        await Clients.All.SendAsync("answer", name, answerUuid);
    }
}
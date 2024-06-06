using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public async Task Answer(string name, string answerUuid) 
    {
        await Clients.All.SendAsync("answer", name, answerUuid);
    }
}
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public Task Answer(
           string name,
           string answerUuid
       ) => Clients.All.SendAsync("answer", name, answerUuid);
    }
}
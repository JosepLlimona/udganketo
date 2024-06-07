using Microsoft.AspNetCore.SignalR;


namespace SignalRChat.Hubs
{
    public class UdgAnketoHub : Hub
    {
        public Task Answer(
           string name,
           string answerUuid
       ) => Clients.All.SendAsync("answer", name, answerUuid);
    }
}
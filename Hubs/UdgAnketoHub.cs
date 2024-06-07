using Microsoft.AspNetCore.SignalR;


namespace SignalRChat.Hubs
{
    public class UdgAnketoHub : Hub
    {
        public async Task Answer(
           string name,
           string answerUuid
       ) => await Clients.All.SendAsync("answer", name, answerUuid);
    }
}
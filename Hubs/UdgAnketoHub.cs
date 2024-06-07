using Microsoft.AspNetCore.SignalR;


namespace SignalRChat.Hubs
{
    public class UdgAnketoHub : Hub
    {
        public async Task JoinRoom(string anketoUuid)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, anketoUuid);
        }
        public async Task LeaveRoom(string anketoUuid)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, anketoUuid);
        }

        public async Task Answer(string anketoUuid, string answerUuid)
        {
            await Clients.Group(anketoUuid).SendAsync("answer", answerUuid);
        }
    }
}
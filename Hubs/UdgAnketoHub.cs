using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

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
    }
}
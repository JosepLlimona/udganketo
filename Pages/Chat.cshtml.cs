using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace udganketo.Pages
{
    public class ChatModel : PageModel, IAsyncDisposable
    {
        private readonly NavigationManager _navigation;
        private HubConnection _hubConnection;

        public List<string> Messages { get; private set; } = new List<string>();
        public string UserInput { get; set; }
        public string MessageInput { get; set; }
        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        public ChatModel(NavigationManager navigation)
        {
            _navigation = navigation;
        }

        public async Task OnGetAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_navigation.ToAbsoluteUri("/chathub"))
                .Build();

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var encodedMsg = $"{user}: {message}";
                Messages.Add(encodedMsg);
                // Trigger a re-render by setting a property or calling a method.
                // You can use a flag or an event to notify the view to re-render.
                ReloadPage();
            });

            await _hubConnection.StartAsync();
        }

        public async Task OnPostSendAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.SendAsync("SendMessage", UserInput, MessageInput);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
            }
        }

        private void ReloadPage()
        {
            // Simple method to force page reload
            Response.Redirect(Request.Path);
        }
    }
}
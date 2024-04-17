using Microsoft.AspNetCore.SignalR;

namespace Checkers.UI2.Hubs
{
    public class CheckersHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

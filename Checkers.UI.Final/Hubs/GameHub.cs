using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Checkers.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendMove(int oldX, int oldY, int newX, int newY)
        {
            await Clients.Others.SendAsync("ReceiveMove", oldX, oldY, newX, newY);
        }
    }
}
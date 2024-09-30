using Microsoft.AspNetCore.SignalR;

namespace MVC_1_Depi.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message+$" {DateTime.Now}");
        }
    }
}

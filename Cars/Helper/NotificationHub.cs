namespace Cars.Helper
{
    using Cars.Models;
    using Cars.NewFolder;
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    public class NotificationHub : Hub
    {
       
        public async Task SendNotification(string userId, string message)


        {
           
            await Clients.User(userId).SendAsync("ReceiveNotification", message);

          
        }

    }
}

using Cars.Helper;
using Cars.Models;
using Cars.NewFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Cars.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly AppDbContext appDbContext;


        public NotificationsController(IHubContext<NotificationHub> hubContext, AppDbContext appDbContext)
        {
            _hubContext = hubContext;
            this.appDbContext = appDbContext;
        }

        [HttpPost("SendNotification")]
        [Authorize(Roles = "UserRole,CompanyRole")]

        public async Task<IActionResult> SendNotification(string Message)
        {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _hubContext.Clients.User(UserId).SendAsync("ReceiveNotification",Message);
            NotificationModel notificationModel = new NotificationModel
            {
                UserId = UserId,
                Message = Message
            };
            appDbContext.Notifications.Add(notificationModel);
            appDbContext.SaveChanges();
            return Ok();
        }
    }
}

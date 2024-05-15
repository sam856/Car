using Cars.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}

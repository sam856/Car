using Cars.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.VeiwModel
{
    public class UserRegesiterVeiwModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfiremPassword { get; set; }

    }



}

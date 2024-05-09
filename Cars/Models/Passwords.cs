using System.ComponentModel.DataAnnotations;

namespace Cars.Models
{
    public class Passwords
    {
        public string NewPassword { get; set; }

        [Compare("Password")]
        public string ConfiremPassword { get; set; }

    }
}

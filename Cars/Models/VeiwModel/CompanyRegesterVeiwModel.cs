using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models.VeiwModel
{
    public class CompanyRegesterVeiwModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfiremPassword { get; set; }


    }
}

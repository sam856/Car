using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        

        public byte[]? Logo { get; set; }
        public byte[]?image { get; set; }
    }
}

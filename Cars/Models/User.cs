using Cars.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models
{
    public class User
    {
        public int Id { get; set; }
        [ForeignKey("applicationUser")]
        public string UserId { get; set; }
        public ICollection<Products> Products { get; set; }
        public ApplicationUser applicationUser { get; set; }

        


    }
}
 
using Cars.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models
{
    public class Company
    {
        public   int Id { get; set; }
        public ICollection<Products> Products { get; set; }


        public ApplicationUser applicationUser { get; set; }
        [ForeignKey("applicationUser")]
        public string UserId { get; set; }
        public string City { get; set; }
        public string Location { get; set; }

    }

}

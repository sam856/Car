using Cars.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models
{
    public class FavouriteCar
    {
        public int Id { get; set; }
        [ForeignKey("user")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Products")]
        public  int ProductId { get; set; }
        public Products Products { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models
{
    public class Images
    {
        public int Id { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public byte[] Image4 { get; set; }

        // Define ProductId as foreign key to Products
        [ForeignKey("Products")]
        public int ProductId { get; set; }

        // Navigation property for the associated product
        public Products Products { get; set; }
    }
}

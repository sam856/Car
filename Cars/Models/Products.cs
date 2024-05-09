using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public string Condition { get; set; }

        // Define ImageId as foreign key to Images
        [ForeignKey("Images")]
        public int ImageId { get; set; }

        

        // Navigation property for the associated company
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        // Navigation property for the associated company
        public Company Company { get; set; }

        // Navigation property for the associated image
        public Images Images { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketArea.ViewModels
{
    public class CreateAdViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage ="{0} must be between {2} and {1} characters.")]
        public string Name { get; set; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string AdCity { get; set; }

        [Required]
        [StringLength(300)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Description { get; set; }

        [Required]
        public string AdCategory { get; set; }





    }
}

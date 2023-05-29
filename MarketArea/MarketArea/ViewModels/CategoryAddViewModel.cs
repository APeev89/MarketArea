using System.ComponentModel.DataAnnotations;

namespace MarketArea.ViewModels
{
    public class CategoryAddViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Name { get; set; }
    }
}

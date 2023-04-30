using System.ComponentModel.DataAnnotations;

namespace MarketArea.ViewModels
{
    public class UserEditViewModel
    {

        public string Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketArea.Data.ModelDb
{
    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateFrom = DateTime.Now;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Text { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateFrom { get; set; }


        public string AdId { get; set; }

        public Ad Ad { get; set; }


        public string UserId { get; set; }

        public IdentityUser User { get; set; }

    }
}

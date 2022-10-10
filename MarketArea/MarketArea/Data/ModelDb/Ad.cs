using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketArea.Data.ModelDb
{
    public class Ad
    {
        public Ad()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new List<Comment>();
            this.Seens = new HashSet<UserSeens>();
            this.Likes = new HashSet<UserLikes>();
            this.UserFavorites = new HashSet<UserFavorite>();
            this.DateFrom = DateTime.Now;
            this.DateTo = DateTime.Now.AddDays(45);
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(300)]
        public string ImageUrl { get; set; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public bool IsArchive { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateFrom { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateTo { get; set; }


        public string CityId { get; set; }
        public City City { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserSeens> Seens { get; set; }
        public ICollection<UserLikes> Likes { get; set; }
        public ICollection<UserFavorite> UserFavorites { get; set; }


    }
}

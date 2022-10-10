using System.ComponentModel.DataAnnotations;

namespace MarketArea.Data.ModelDb
{
    public class City
    {
        public City()
        {
            this.Ads = new HashSet<Ad>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Ad> Ads { get; set; }

    }
}
